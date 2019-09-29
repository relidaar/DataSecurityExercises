using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSecurity.Lab1_5
{
    static class Extensions
    {
        public static string GetBinary(this int number)
        {
            if (number == 0) return "";

            int remainder = number % 2;
            number /= 2;

            return GetBinary(number) + remainder;
        }

        public static string ToFullBinary(this string value, int length=8)
        {
            while (value.Length < length) value = '0' + value;
            return value;
        }

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
    }
}
