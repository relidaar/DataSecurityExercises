using System;
using System.Numerics;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_2
{
    public class RsaSender
    {
        private readonly int _lowerBound;
        private readonly int _upperBound;

        private int _d;

        public RsaSender(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public int e { get; private set; }
        public int n { get; private set; }
        public BigInteger k { get; private set; }

        public void GenerateKeys()
        {
            var p = MathExtensions.GetPrime(_upperBound, _lowerBound);
            var q = MathExtensions.GetPrime(_upperBound, _lowerBound);

            n = p * q;

            var m = (p - 1) * (q - 1);
            e = GetE(m);
            _d = GetD(m);
        }

        public void CalculateK(BigInteger r)
        {
            k = BigInteger.Pow(r, _d) % n;
        }

        private int GetD(int m)
        {
            var value = 0;
            while (value * e % m != 1) value++;

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
        private BigInteger _k = 19229;
        public BigInteger r { get; private set; }

        public void GenerateK(int n)
        {
            _k = new Random().Next(1, n);
        }

        public void CalculateR(int e, int n)
        {
            r = BigInteger.Pow(_k, e) % n;
        }

        public bool Check(BigInteger k)
        {
            return _k == k;
        }
    }
}