using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_5
{
    internal class RsaAlgorithm : IEncoder
    {
        private int _d;
        private int _e;
        private int _m;
        private int _n;
        private int _p;
        private int _q;

        public RsaAlgorithm(int lowerBound, int upperBound)
        {
            if (lowerBound <= 0 || upperBound <= 0)
                throw new Exception("Lower or upper bound cannot be less than or equal to zero");
            if (lowerBound >= upperBound)
                throw new Exception("Lower bound cannot be greater than or equal to upper bound");

            GenerateKey(lowerBound, upperBound);
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new List<BigInteger>();
            foreach (var symbol in message.Select(x => (int) x)) result.Add(BigInteger.Pow(symbol, _e) % _n);

            return string.Join(" ", result).Trim();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in encryptedMessage.Split(' '))
            {
                var number = int.Parse(symbol);
                var index = Convert.ToInt32((BigInteger.Pow(number, _d) % _n).ToString());
                result.Append((char) index);
            }

            return result.ToString();
        }

        private void GenerateKey(int lowerBound, int upperBound)
        {
            _p = MathExtensions.GetPrime(upperBound);
            _q = MathExtensions.GetPrime(upperBound);

            _n = _p * _q;
            _m = (_p - 1) * (_q - 1);
            _d = GetD(lowerBound, upperBound);
            _e = GetE(lowerBound);
        }

        private int GetD(int lowerBound, int upperBound)
        {
            var rnd = new Random();
            var d = rnd.Next(lowerBound, upperBound);
            while (MathExtensions.GetNod(d, _m) != 1) d = rnd.Next(lowerBound, upperBound);

            return d;
        }

        private int GetE(int lowerBound)
        {
            var e = lowerBound;
            while (true)
            {
                if (e * _d % _m == 1) break;
                e++;
            }

            return e;
        }
    }
}