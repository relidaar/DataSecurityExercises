using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using DataSecurity.Lab1_5.Encoders.Interfaces;

namespace DataSecurity.Lab1_5.Encoders.Implementations
{
    class ElGamalAlgorithm : IEncoder
    {
        public string Name => "ElGamal algorithm";

        private int _p;
        private int _g;
        private int _x;
        private BigInteger _y;
        private readonly int _range;

        public ElGamalAlgorithm(int range)
        {
            if (range <= 0) return;

            _range = range;
        }

        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            GenerateKey();

            var a = new List<BigInteger>();
            var b = new List<BigInteger>();
            foreach (var c in message)
            {
                int k = new Random().Next(2, _p - 1);
                a.Add(BigInteger.Pow(_g, k) % _p);
                b.Add(BigInteger.Pow(_y, k) * c % _p);
            }

            return string.Join(" ", a) + " @@@ " + string.Join(" ", b);
        }

        public string Decrypt(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return null;

            var temp = encryptedMessage.Split(" @@@ ");
            var listA = temp[0].Split(" ");
            var listB = temp[1].Split(" ");
            var encrypted = listA.Zip(listB, (a, b) => new { A = BigInteger.Parse(a), B = BigInteger.Parse(b)});
       
            string result = "";
            foreach (var pair in encrypted)
            {
                var index = pair.B * BigInteger.Pow(pair.A, _p - 1 - _x) % _p;
                result += (char)index;
            }

            return result;
        }

        private void GenerateKey()
        {
            _p = Extensions.GetPrime(127 + _range, 127);
            _g = GetG();
            _x = new Random().Next(2, _p);
            _y = BigInteger.Pow(_g, _x) % _p;
        }

        private int GetG()
        {
            int g = 2;
            while (!IsPrimitive(g)) g++;

            return g;
        }

        private bool IsPrimitive(int g)
        {
            for (int i = 1; i < _p - 1; i++)
            {
                if (BigInteger.Pow(g, i) % _p == 1) return false;
            }

            return true;
        }
    }
}
