using System;

namespace DataSecurity.Lab2_2
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
    }
}
