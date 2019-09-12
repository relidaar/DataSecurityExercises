using System;
using System.Text.RegularExpressions;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    class ReverseBinary : IEncoder
    {
        public string Name => "Reverse binary";

        public string Encrypt(string number)
        {
            if (string.IsNullOrEmpty(number)) return null;
            if (!int.TryParse(number, out int num)) return null;

            string result = GetBinary(Math.Abs(num), Math.Abs(num));
            while (result.Length % 8 != 0)
            {
                result = '0' + result;
            }
            result = Regex.Replace(result, ".{4}", "$0 ");

            if (num >= 0) return result;

            for (int i = 0; i < result.Length; i++)
            {
                var digit = result[i];

                if (digit == '0') result = result.ReplaceAt(i, '1');
                else if (digit == '1') result = result.ReplaceAt(i, '0');
            }

            return result;
        }

        public string Decrypt(string encryptedNumber)
        {
            throw new NotImplementedException();
        }

        private string GetBinary(int number, int quotient)
        {
            if (quotient == 0) return "";

            number = quotient;
            int remainder = number % 2;
            quotient = number / 2;

            return GetBinary(number, quotient) + remainder;
        }
    }
}
