using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab2_4
{
    public class Crc : IEncoder
    {
        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder(); 
            foreach (var binary in message.Select(c => Convert.ToString(c, 2)))
            {
                var extendedBinary = binary.PadRight(binary.Length + 4, '0');
                var current = extendedBinary.Substring(0, 5);

                for (int i = 5; i < extendedBinary.Length; i++)
                {
                    current = Xor(current, current[0] == '1' ? "10011" : "00000");

                    current += extendedBinary[i];
                }

                result.Append($"{binary} {current.Remove(current.Length-1)} | ");
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            throw new NotImplementedException();
        }

        public string Xor(string a, string b)
        {
            var result = new StringBuilder();
            for (int i = 1; i < 5; i++)
            {
                if (a[i] == '1' && b[i] == '0' || a[i] == '0' && b[i] == '1') result.Append("1");
                else result.Append('0');
            }

            return result.ToString();
        }
    }
}
