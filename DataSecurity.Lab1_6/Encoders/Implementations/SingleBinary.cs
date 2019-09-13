using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    class SingleBinary : BaseEncoder, IEncoder
    {
        public string Name => "Single binary";

        public string Encrypt(string number)
        {
            if (string.IsNullOrEmpty(number)) return null;
            if (number.Equals("0")) return null;
            if (number.Equals("-0")) return null;
            if (!double.TryParse(number, out double num)) return null;

            int integralPart = Math.Abs((int)num);
            double fractionalPart = Math.Abs(num) - integralPart;

            string integral = integralPart == 0 ? "0" : GetBinary(integralPart);
            string fractional = GetFractionalBinary(fractionalPart);

            char sign = num < 0 ? '1' : '0';

            string mantissa = integral.Remove(0, 1) + fractional;

            int e = integral.Length - 1;
            if (integral == "0")
            {
                e = -1;
                foreach (var digit in fractional)
                {
                    if (digit == '1') break;
                    e--;
                }

                mantissa = fractional.Remove(0, Math.Abs(e));
            }

            string exponent = GetBinary(127 + e);
            while (exponent.Length < 8) exponent = '0' + exponent;
            exponent = Regex.Replace(exponent, ".{4}", "$0 ");

            while (mantissa.Length < 23) mantissa += '0';
            mantissa = Regex.Replace(mantissa, ".{4}", "$0 ");

            return $"{sign}  {exponent} {mantissa} ";
        }

        public string Decrypt(string encryptedNumber)
        {
            throw new NotImplementedException();
        }

        private string GetFractionalBinary(double number)
        {
            if (number == 0) return "0";
            string fractional = "";
            while (fractional.Length < 8)
            {
                number *= 2;
                fractional += (int)number;
                number = number < 1 ? number : number - (int)number;
            }

            return fractional;
        }
    }
}
