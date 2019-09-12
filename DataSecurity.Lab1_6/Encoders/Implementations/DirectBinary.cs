using System;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    class DirectBinary : IEncoder
    {
        public string Name => "Direct binary";

        public string Encrypt(string number)
        {
            if (string.IsNullOrEmpty(number)) return null;
            if (!int.TryParse(number, out int num)) return null;

            string result = GetBinary(Math.Abs(num), Math.Abs(num));
            while (result.Length % 8 != 0)
            {
                result = '0' + result;
            }

            if (num >= 0) return result;

            result = result.Remove(0, 1);
            result = '1' + result;

            return result;
        }

        public string Decrypt(string encryptedNumber)
        {
            if (string.IsNullOrEmpty(encryptedNumber)) return null;

            var firstDigit = encryptedNumber.Substring(0, 1);
            encryptedNumber = encryptedNumber.Remove(0, 1);

            int result = 0;
            int n = encryptedNumber.Length;
            for (var i = 0; i < n; i++)
            {
                var digit = encryptedNumber[i] - '0';
                result += (int)(digit * Math.Pow(2, n - 1 - i));
            }

            if (firstDigit == "1") result = -result;

            return result.ToString();
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
