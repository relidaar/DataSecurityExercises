using System;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_6
{
    internal class FloatBinaryEncoder : IEncoder
    {
        public string Encode(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new NullReferenceException();
            if (number.Equals("0")) throw new Exception("Number cannot be equal to 0");
            if (number.Equals("-0")) throw new Exception("Number cannot be equal to -0");
            var num = Convert.ToDouble(number);

            var integralPart = Math.Abs((int) num);
            var fractionalPart = Math.Abs(num) - integralPart;

            var integral = integralPart == 0 ? "0" : integralPart.GetBinary();
            var fractional = fractionalPart.GetFractionalBinary();

            var sign = num < 0 ? '1' : '0';

            var mantissa = integral.Remove(0, 1) + fractional;

            var e = integral.Length - 1;
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

            var exponent = e.GetBinary().PadLeft(7, '0');
            mantissa = mantissa.PadRight(15, '0');

            return $"{sign} {exponent} {sign} {mantissa} ";
        }

        public string Decode(string encryptedNumber)
        {
            throw new NotImplementedException();
        }
    }
}