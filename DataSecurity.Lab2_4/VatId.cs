using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab2_4
{
    public class VatId : IEncoder
    {
        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();
            if (message.Length < 10) throw new Exception("Length must be at least 10");

            var values = message.Substring(0, 10).Select(c => (int)c).ToList();
            Console.WriteLine("Values: " + string.Join(" ", values));

            var n10 = (2 * values[0] + 4 * values[1] + 10 * values[2] +
                       3 * values[3] + 5 * values[4] + 9 * values[5] + 
                       4 * values[6] + 6 * values[7] + 8 * values[8]) % 11 % 10;

            return Convert.ToString(n10);
        }

        public string Decode(string encryptedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
