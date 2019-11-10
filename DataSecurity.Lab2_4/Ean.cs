using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab2_4
{
    public class Ean : IEncoder
    {
        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();
            if (message.Length < 15) throw new Exception("Length must be at least 13");

            var values = message.Substring(0, 13).Select(c => (int)c).ToList();
            Console.WriteLine("Values: " + string.Join(" ", values));

            var se = 0;
            var so = 0;
            for (int i = 0; i < values.Count - 1; i++)
            {
                if ((i + 1) % 2 == 0) se += values[i];
                else so += values[i];
            }
            se = 3 * se;

            Console.WriteLine($"Se = {se}, So = {so}");

            int cd = 0;
            while ((se + so + cd) % 10 != 0) cd++;

            return Convert.ToString(cd);
        }

        public string Decode(string encryptedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
