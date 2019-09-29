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
        private int _m;
        private int _p;
        private int _q;
        private readonly int _lowerBound;
        private readonly int _upperBound;

        public RsaAlgorithm(int lowerBound, int upperBound)
        {
            if (lowerBound <= 0 || upperBound <= 0) return;
            if (lowerBound >= upperBound) return;

            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            GenerateKey();

            string result = "";
            foreach (var symbol in message)
            {
                var index = (int)symbol;
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
                result += (char) index;
            }

            return result;
        }

        private void GenerateKey()
        {
            _p = GetPrime();
            _q = GetPrime();

            _n = _p * _q;
            _m = (_p - 1) * (_q - 1);
            _d = GetD();
            _e = GetE();
        }

        private int GetPrime()
        {
            var rnd = new Random();
            int number = rnd.Next(_lowerBound, _upperBound);
            while (!number.IsPrime()) number = rnd.Next(_lowerBound, _upperBound);

            return number;
        }

        private int GetD()
        {
            var rnd = new Random();
            int d = rnd.Next(_lowerBound, _upperBound);
            while (Extensions.GetNod(d, _m) != 1) d = rnd.Next(_lowerBound, _upperBound);

            return d;
        }

        private int GetE()
        {
            int e = _lowerBound;
            while (true)
            {
                if (e * _d % _m == 1) break;
                e++;
            }

            return e;
        }
    }
}
