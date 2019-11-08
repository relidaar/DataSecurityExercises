using System;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_6
{
    internal class DirectBinaryEncoder : IEncoder
    {
        public string Encode(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new NullReferenceException();
            if (number.Equals("0")) return "00000000";
            if (number.Equals("-0")) return "10000000";
            var num = Convert.ToInt32(number);

            var result = Math.Abs(num).GetBinary().PadLeft(8, '0');

            return num < 0 ? result.ReplaceAt(0, '1') : result;
        }

        public string Decode(string encryptedNumber)
        {
            throw new NotImplementedException();
        }
    }
}