using System;
using System.Text.RegularExpressions;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    class DirectBinary : BaseEncoder, IEncoder
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
            result = Regex.Replace(result, ".{4}", "$0 ");

            return num < 0 ? result.ReplaceAt(0, '1') : result;
        }

        public string Decrypt(string encryptedNumber)
        {
            throw new NotImplementedException();
        }
    }
}
