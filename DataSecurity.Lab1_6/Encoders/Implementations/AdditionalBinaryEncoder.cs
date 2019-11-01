using System;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    internal class AdditionalBinaryEncoder : IEncoder
    {
        public string Encode(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new NullReferenceException();
            if (number.Equals("0")) return "00000000";
            if (number.Equals("-0")) throw new Exception("Number cannot be equal to -0");
            var num = Convert.ToInt32(number);

            var result = Math.Abs(num).GetBinary().PadLeft(8, '0');

            if (num >= 0) return result;

            for (var i = 0; i < result.Length; i++)
            {
                var digit = result[i];

                if (digit == '0') result = result.ReplaceAt(i, '1');
                else if (digit == '1') result = result.ReplaceAt(i, '0');
            }

            result = Convert.ToString(Convert.ToInt32(result, 2) + 1, 2);

            return result;
        }

        public string Decode(string encryptedNumber)
        {
            throw new NotImplementedException();
        }
    }
}