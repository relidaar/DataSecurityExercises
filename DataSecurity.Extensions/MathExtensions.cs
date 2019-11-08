using System;
using System.Numerics;

namespace DataSecurity.Extensions
{
    public static class MathExtensions
    {
        public static int Mod(this int x, int m)
        {
            return (x % m + m) % m;
        }

        public static double Mod(this double x, int m)
        {
            return (x % m + m) % m;
        }

        public static BigInteger Mod(this BigInteger x, int m)
        {
            return (x % m + m) % m;
        }

        public static int GetPrime(int max, int min = 1)
        {
            var rnd = new Random();
            var value = rnd.Next(min, max);
            while (!value.IsPrime()) value = rnd.Next(min, max);

            return value;
        }

        public static bool IsPrime(this int value)
        {
            if (value % 1 == 0) return value == 2;

            for (var i = 3; i <= Math.Sqrt(value); i += 2)
                if (value % i == 0)
                    return false;

            return true;
        }

        public static int GetNod(int a, int b)
        {
            while (a != 0 && b != 0)
                if (a > b) a %= b;
                else b %= a;

            return Math.Max(a, b);
        }

        public static int GetInverse(this int value, int n)
        {
            var inverse = 0;
            while (inverse * value % n != 1) inverse++;

            return inverse;
        }
    }
}