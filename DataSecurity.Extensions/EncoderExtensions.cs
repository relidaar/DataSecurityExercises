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
            for (int i = 0; i < n; i++) yield return (char)(i + shift);
        }

        public static string ReplaceAt(this string value, int index, char newChar) => 
            value.Length <= index ? value : string.Concat(value.Select((c, i) => i == index ? newChar : c));

        public static string GetBinary(this int value)
        {
            if (value == 0) return "";

            int remainder = value % 2;
            value /= 2;

            return GetBinary(value) + remainder;
        }

        public static string GetBinary(this string data, int width = 8)
        {
            var result = new StringBuilder();
            foreach (var c in data.ToCharArray())
            {
                result.Append(Convert.ToString(c, 2).PadLeft(width, '0'));
            }

            return result.ToString();
        }

        public static string GetFractionalBinary(this double value)
        {
            if (value == 0) return "0";

            var fractional = new StringBuilder();
            while (fractional.Length < 8)
            {
                value *= 2;
                fractional.Append((int)value);
                value = value < 1 ? value : value - (int)value;
            }

            return fractional.ToString();
        }
    }
}
