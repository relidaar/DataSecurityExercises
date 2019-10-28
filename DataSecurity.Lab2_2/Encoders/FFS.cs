using System;
using System.Numerics;

namespace DataSecurity.Lab2_2.Encoders
{
    public class FfsSender
    {
        public int z { get; private set; }
        public int r { get; private set; } = 19229;
        public int y { get; private set; }

        public void GenerateR(int n) => r = new Random().Next(1, n);

        public void CalculateZ(int n) => z = (int)(Math.Pow(r, 2) % n);

        public void CalculateY(int n, int s) => y = r * s % n;
    }

    public class FfsReceiver
    {
        public int GetRandomBit() => new Random().Next(0, 2);

        public bool Check(int z, int r, int n) => z == (int)(Math.Pow(r, 2) % n);

        public bool Check(int z, int y, int v, int n) => z == (int)(Math.Pow(y, 2) * v % n);
    }

    public class FfsProvider
    {
        public int n { get; private set; }
        public int v { get; private set; }
        public int s { get; private set; }
        private readonly int _lowerBound;
        private readonly int _upperBound;

        public FfsProvider(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public void GenerateKeys()
        {
            var p = Extensions.GetPrime(_upperBound, _lowerBound);
            var q = Extensions.GetPrime(_upperBound, _lowerBound);

            n = p * q;

            v = GetV();
            var inverse = GetInverse(v);

            s = GetS(inverse);
        }

        private int GetV()
        {
            int value = 0;
            int x = new Random().Next(1, n + 1);
            while (true)
            {
                if (value == BigInteger.Pow(x, 2) % n && GetInverse(value) != 0) break;

                value++;
            }

            return value;
        }

        private int GetInverse(int value)
        {
            int inverse = 0;
            while (inverse * value % n != 1) inverse++;

            return inverse;
        }

        private int GetS(int inverse)
        {
            int value = 1;
            while (BigInteger.Pow(value, 2) % n != inverse) value++;

            return value;
        }
    }
}
