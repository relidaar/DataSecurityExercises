using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_3
{
    public class RsaSender
    {
        private readonly int _lowerBound;
        private readonly int _upperBound;

        private int _d;
        private BigInteger _h = 19229;
        public int E;
        public int N;
        public BigInteger S;

        public RsaSender(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public void CalculateH(string input)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                _h = new BigInteger(hash.Concat(new byte[] {0}).ToArray());
            }
        }

        public BigInteger CalculateS()
        {
            return S = BigInteger.Pow(_h, _d) % N;
        }

        public void GenerateKeys()
        {
            var p = MathExtensions.GetPrime(_upperBound, _lowerBound);
            var q = MathExtensions.GetPrime(_upperBound, _lowerBound);

            N = p * q;

            var m = (p - 1) * (q - 1);
            E = GetE(m);
            _d = GetD(m);
        }

        private int GetD(int m)
        {
            var value = 0;
            while (value * E % m != 1) value++;

            return value;
        }

        private int GetE(int m)
        {
            var rnd = new Random();
            var value = rnd.Next(_lowerBound, _upperBound);
            while (MathExtensions.GetNod(value, m) != 1) value = rnd.Next(_lowerBound, _upperBound);

            return value;
        }
    }

    public class RsaReceiver
    {
        private BigInteger _h = 19229;

        public void CalculateH(string input)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                _h = new BigInteger(hash.Concat(new byte[] {0}).ToArray());
            }
        }

        public bool Check(BigInteger s, int e, int n)
        {
            return _h == BigInteger.Pow(s, e) % n;
        }
    }
}