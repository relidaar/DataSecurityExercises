using System;
using System.Numerics;

namespace DataSecurity.Lab2_2.Encoders
{
    public class SchnorrSender
    {
        public int p { get; private set; }
        private int _q;

        public int g { get; private set; }
        public int y { get; private set; }
        private int _x;

        public BigInteger r { get; private set; }
        public int s { get; private set; }
        private int _k = 19299;

        private readonly int _lowerBound;
        private readonly int _upperBound;

        public SchnorrSender(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public void GenerateKeys()
        {
            while (true)
            {
                p = Extensions.GetPrime(_upperBound, _lowerBound);
                _q = Extensions.GetPrime(_upperBound, _lowerBound);

                if ((p - 1) % _q == 0) break;
            }

            _x = new Random().Next(1, _q - 1);
            g = GetG();
            y = GetY();
        }

        private int GetG()
        {
            int g = 0;
            while ((int)Math.Pow(g, _q) % p != 1) g++;

            return g;
        }

        private int GetY()
        {
            int y = 1;
            while ((int)Math.Pow(g, _x) * y % p != 1) y++;

            return y;
        }

        public void GenerateK() => _k = new Random().Next(1, _q);

        public void CalculateR() => r = BigInteger.Pow(g, _k) % p;

        public void CalculateS(int e) => s = (_k + _x * e) % _q;
    }

    public class SchnorrReceiver
    {
        public int e { get; private set; }

        private readonly int _t;

        public SchnorrReceiver(int t) => _t = t;

        public void GenerateE() => e = new Random().Next(0, (int)(Math.Pow(2, _t) - 1));

        public bool Check(int s, BigInteger r, int p, int g, int y) => 
            r == BigInteger.Pow(g, s) * BigInteger.Pow(y, e) % p;
    }
}
