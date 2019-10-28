using System;
using System.Numerics;

namespace DataSecurity.Lab2_2.Encoders
{
    public class RsaSender
    {
        public int e { get; private set; }
        public int n { get; private set; }
        public BigInteger k { get; private set; }

        private int _d;
        private readonly int _lowerBound;
        private readonly int _upperBound;

        public RsaSender(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public void GenerateKeys()
        {
            var p = Extensions.GetPrime(_upperBound, _lowerBound);
            var q = Extensions.GetPrime(_upperBound, _lowerBound);

            n = p * q;

            int m = (p - 1) * (q - 1);
            e = GetE(m);
            _d = GetD(m);
        }

        public void CalculateK(BigInteger r) => k = BigInteger.Pow(r, _d) % n;

        private int GetD(int m)
        {
            int value = 0;
            while (value * e % m != 1) value++;

            return value;
        }

        private int GetE(int m)
        {
            var rnd = new Random();
            int value = rnd.Next(_lowerBound, _upperBound);
            while (Extensions.GetNod(value, m) != 1) value = rnd.Next(_lowerBound, _upperBound);

            return value;
        }
    }

    public class RsaReceiver
    {
        public BigInteger r { get; private set; }

        private BigInteger _k = 19229;

        public void GenerateK(int n) => _k = new Random().Next(1, n);

        public void CalculateR(int e, int n) => r = BigInteger.Pow(_k, e) % n;

        public bool Check(BigInteger k) => _k == k;
    }
}
