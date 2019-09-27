using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using DataSecurity.Lab1_5.Encoders.Interfaces;

namespace DataSecurity.Lab1_5.Encoders.Implementations
{
    class RsaAlgorithm : BaseEncoder, IEncoder
    {
        public string Name => "Rsa algorithm";

        private int _e;
        private int _d;
        private int _n;

        public RsaAlgorithm(int lowerBound, int upperBound) => GenerateKey(lowerBound, upperBound);

        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;
            message = message.ToUpper();

            string result = "";
            foreach (var symbol in message.Where(symbol => Characters.Contains(symbol)))
            {
                var index = Characters.IndexOf(symbol);
                result += BigInteger.Pow(index, _e) % _n + " ";
            }

            return result.Trim();
        }

        public string Decrypt(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return null;

            string result = "";
            foreach (var symbol in encryptedMessage.Split(" "))
            {
                var number = int.Parse(symbol);
                var index = Convert.ToInt32((BigInteger.Pow(number, _d) % _n).ToString());
                result += Characters[index];
            }

            return result;
        }

        private void GenerateKey(int lowerBound, int upperBound)
        {
            var p = GetPrime(lowerBound, upperBound);
            var q = GetPrime(lowerBound, upperBound);

            _n = p * q;
            var m = (p - 1) * (q - 1);
            _d = GetD(lowerBound, upperBound, m);
            _e = GetE(lowerBound, m);
        }

        private static int GetPrime(int lowerBound, int upperBound)
        {
            var rnd = new Random();
            int number = rnd.Next(lowerBound, upperBound);
            while (!IsPrime(number)) number = rnd.Next(lowerBound, upperBound);

            return number;
        }

        private int GetD(int lowerBound, int upperBound, int m)
        {
            var rnd = new Random();
            int d = rnd.Next(lowerBound, upperBound);
            while (GetNod (d, m) != 1) d = rnd.Next(lowerBound, upperBound);

            return d;
        }

        private int GetE(int n, int m)
        {
            int e = n;
            while (true)
            {
                if (e * _d % m == 1) break;
                e++;
            }

            return e;
        }

        private static int GetNod(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b) a %= b;
                else b %= a;
            }

            return Math.Max(a, b);
        }

        private static bool IsPrime(int number)
        {
            if ((number & 1) == 0) return number == 2;

            int limit = (int)Math.Sqrt(number);

            for (int i = 3; i <= limit; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}
