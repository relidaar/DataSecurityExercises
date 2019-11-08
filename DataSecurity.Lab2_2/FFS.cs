using System;
using System.Numerics;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_2
{
    public class FfsSender
    {
        public int z { get; private set; }
        public int r { get; private set; } = 19229;
        public int y { get; private set; }

        public void GenerateR(int n)
        {
            r = new Random().Next(1, n);
        }

        public void CalculateZ(int n)
        {
            z = (int) (Math.Pow(r, 2) % n);
        }

        public void CalculateY(int n, int s)
        {
            y = r * s % n;
        }
    }

    public class FfsReceiver
    {
        public int GetRandomBit()
        {
            return new Random().Next(0, 2);
        }

        public bool Check(int z, int r, int n)
        {
            return z == (int) (Math.Pow(r, 2) % n);
        }

        public bool Check(int z, int y, int v, int n)
        {
            return z == (int) (Math.Pow(y, 2) * v % n);
        }
    }

    public class FfsProvider
    {
        private readonly int _lowerBound;
        private readonly int _upperBound;

        public FfsProvider(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public int n { get; private set; }
        public int v { get; private set; }
        public int s { get; private set; }

        public void GenerateKeys()
        {
            var p = MathExtensions.GetPrime(_upperBound, _lowerBound);
            var q = MathExtensions.GetPrime(_upperBound, _lowerBound);

            n = p * q;

            v = GetV();
            var inverse = v.GetInverse(n);

            s = GetS(inverse);
        }

        private int GetV()
        {
            var value = 0;
            var x = new Random().Next(1, n + 1);
            while (true)
            {
                if (value == BigInteger.Pow(x, 2) % n && value.GetInverse(n) != 0) break;

                value++;
            }

            return value;
        }

        private int GetS(int inverse)
        {
            var value = 1;
            while (BigInteger.Pow(value, 2) % n != inverse) value++;

            return value;
        }
    }
}