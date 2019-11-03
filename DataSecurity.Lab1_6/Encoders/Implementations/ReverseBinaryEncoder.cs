using System;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    internal class ReverseBinaryEncoder : IEncoder
    {
        public string Encode(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new NullReferenceException();
            if (number.Equals("0")) return "00000000";
            if (number.Equals("-0")) return "11111111";
            var num = Convert.ToInt32(number);

            var result = Math.Abs(num).GetBinary().PadLeft(8, '0');

            if (num >= 0) return result;

            result = Convert.ToString(~Convert.ToInt32(result, 2), 2);

            return result.Substring(result.Length - 8);
        }

        public string Decode(string encryptedNumber)
        {
            throw new NotImplementedException();
        }
    }
}