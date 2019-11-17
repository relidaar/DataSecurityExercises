using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_6
{
    public class Gamma
    {
        private readonly string _secret;
        public List<string> Gammas { get; private set; }

        public Gamma(string input) => _secret = input.GetBinary();

        public List<string> GenerateGammas()
        {
            Gammas = new List<string>();
            var rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                var gamma = new StringBuilder();
                for (int j = 0; j < _secret.Length/8; j++)
                {
                    gamma.Append(rnd.Next(94, 127).GetBinary().PadLeft(8, '0'));
                }

                Gammas.Add(gamma.ToString());
            }

            return Gammas;
        }

        public string Encode() => Gammas.Aggregate(_secret, (current, gamma) => current.Xor(gamma));

        public string Decode(string cipherCode)
        {
            var binary = Gammas.Aggregate(cipherCode, (current, gamma) => current.Xor(gamma));
            var result = new StringBuilder();

            for (int i = 0; i < binary.Length; i+=8)
            {
                var c = Convert.ToInt32(binary.Substring(i, 8), 2);
                result.Append((char) c);
            }

            return result.ToString();
        }
    }
}
