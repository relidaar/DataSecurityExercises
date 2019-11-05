using System;
using System.Numerics;

namespace DataSecurity.Lab2_3
{
    static class Extensions
    {
        public static int GetNod(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b) a %= b;
                else b %= a;
            }

            return Math.Max(a, b);
        }

        public static int GetInverse(this int value, int n)
        {
            int inverse = 0;
            while (inverse * value % n != 1) inverse++;

            return inverse;
        }

        public static bool IsPrime(this int number)
        {
            if ((number & 1) == 0) return number == 2;

            int limit = (int)Math.Sqrt(number);

            for (int i = 3; i <= limit; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        public static int GetPrime(int max, int min = 1)
        {
            var rnd = new Random();
            int number = rnd.Next(min, max);
            while (!number.IsPrime()) number = rnd.Next(min, max);

            return number;
        }

        public static int Mod(this int x, int m) => (x % m + m) % m;
    }
}
