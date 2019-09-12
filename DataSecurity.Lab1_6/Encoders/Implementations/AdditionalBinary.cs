using System;
using System.Text.RegularExpressions;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    class AdditionalBinary : IEncoder
    {
        public string Name => "Additional binary";

        public string Encrypt(string number)
        {
            if (string.IsNullOrEmpty(number)) return null;
            if (!int.TryParse(number, out int num)) return null;

            string result = GetBinary(Math.Abs(num), Math.Abs(num));
            while (result.Length % 8 != 0)
            {
                result = '0' + result;
            }

            if (num >= 0) return Regex.Replace(result, ".{4}", "$0 ");

            for (int i = 0; i < result.Length; i++)
            {
                var digit = result[i];

                switch (digit)
                {
                    case '0':
                        result = result.ReplaceAt(i, '1');
                        break;
                    case '1':
                        result = result.ReplaceAt(i, '0');
                        break;
                }
            }

            int n = result.Length - 1;
            result = result[n] == '0' ? result.ReplaceAt(n, '1') : 
                Convert.ToString(Convert.ToInt32(result, 2) + 1, 2);

            return Regex.Replace(result, ".{4}", "$0 ");
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
