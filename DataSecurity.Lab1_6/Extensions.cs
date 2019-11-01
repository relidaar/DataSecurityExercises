using System.Linq;
using System.Text;

namespace DataSecurity.Lab1_6
{
    public static class Extensions
    {
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

        public static string GetFractionalBinary(this double number)
        {
            if (number == 0) return "0";

            var fractional = new StringBuilder();
            while (fractional.Length < 8)
            {
                number *= 2;
                fractional.Append((int) number);
                number = number < 1 ? number : number - (int) number;
            }

            return fractional.ToString();
        }
    }
}