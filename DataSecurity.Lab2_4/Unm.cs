using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab2_4
{
    public class Unm : IEncoder
    {
        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();
            if (message.Length < 10) throw new Exception("Length must be at least 5");

            var values = message.Substring(0, 5).Select(c => (int)c).ToList();
            Console.WriteLine("Values: " + string.Join(" ", values));

            var n5 = 0;
            for (int i = 0; i < values.Count-1; i++) n5 += (i + 1) * values[i];
            n5 %= 11;

            if (n5 < 10) return Convert.ToString(n5);

            n5 = 0;
            for (int i = 0; i < values.Count - 1; i++) n5 += (i + 3) * values[i];
            n5 %= 11;

            return Convert.ToString(n5 < 10 ? n5 : 0);
        }

        public string Decode(string encryptedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
