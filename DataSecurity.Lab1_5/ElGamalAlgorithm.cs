using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_5
{
    class ElGamalAlgorithm : IEncoder
    {
        private int _p;
        private int _g;
        private int _x;
        private BigInteger _y;

        public ElGamalAlgorithm(int range)
        {
            if (range <= 0) throw new Exception("Range cannot less than or equal to zero");

            GenerateKey(range);
        }

        public string Encode(string message)
        {
            if (message == null) return null;

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

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            var temp = encryptedMessage.Split(" @@@ ");
            var listA = temp[0].Split(" ");
            var listB = temp[1].Split(" ");
            var encrypted = listA.Zip(listB, (a, b) => new { A = BigInteger.Parse(a), B = BigInteger.Parse(b)});
       
            var result = new StringBuilder();
            foreach (var pair in encrypted)
            {
                var index = pair.B * BigInteger.Pow(pair.A, _p - 1 - _x) % _p;
                result.Append((char)index);
            }

            return result.ToString();
        }

        private void GenerateKey(int range)
        {
            _p = MathExtensions.GetPrime(127 + range, 127);
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
