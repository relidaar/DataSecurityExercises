using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSecurity.Extensions
{
    public static class EncoderExtensions
    {
        public static IEnumerable<char> GenerateAlphabet(int n, int shift)
        {
            for (var i = 0; i < n; i++) yield return (char) (i + shift);
        }

        public static string ReplaceAt(this string value, int index, char newChar)
        {
            return value.Length <= index ? value : string.Concat(value.Select((c, i) => i == index ? newChar : c));
        }

        public static string GetBinary(this int value)
        {
            if (value == 0) return "";

            var remainder = value % 2;
            value /= 2;

            return GetBinary(value) + remainder;
        }

        public static string GetBinary(this string data, int width = 8, string separator = "")
        {
            var result = new StringBuilder();
            foreach (var c in data.ToCharArray()) 
                result.Append(Convert.ToString(c, 2).PadLeft(width, '0') + separator);

            return result.ToString();
        }

        public static string GetFractionalBinary(this double value)
        {
            if (value == 0) return "0";

            var fractional = new StringBuilder();
            while (fractional.Length < 8)
            {
                value *= 2;
                fractional.Append((int) value);
                value = value < 1 ? value : value - (int) value;
            }

            return fractional.ToString();
        }

        public static string Xor(this string a, string b)
        {
            var result = new StringBuilder();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '1' && b[i] == '0' || a[i] == '0' && b[i] == '1') result.Append("1");
                else result.Append('0');
            }

            return result.ToString();
        }
    }
}