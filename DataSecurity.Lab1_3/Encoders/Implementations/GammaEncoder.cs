using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_3.Encoders.Interfaces;

namespace DataSecurity.Lab1_3.Encoders.Implementations
{
    class GammaEncoder : IEncoder
    {
        private readonly int[] _gamma;
        private readonly string _characters;

        public GammaEncoder(int gammaCount)
        {
            if (gammaCount <= 0) throw new Exception("Gamma cannot be less than or equal to zero");

            _gamma = GenerateGamma(gammaCount).ToArray();

            var characters = new StringBuilder();
            for (int i = 0; i < 94; i++) characters.Append((char) (i + 32));

            _characters = characters.ToString();
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            int gammaIndex = 0;

            var result = new StringBuilder();
            foreach (var symbol in message)
            {
                int index = Mod(_characters.IndexOf(symbol) + _gamma[gammaIndex], _characters.Length);

                result.Append(_characters[index]);
                gammaIndex++;

                if (gammaIndex + 1 == _gamma.Length) gammaIndex = 0;
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            int gammaIndex = 0;

            var result = new StringBuilder();
            foreach (var symbol in encryptedMessage)
            {
                int index = Mod(_characters.IndexOf(symbol) + _characters.Length - _gamma[gammaIndex], 
                    _characters.Length);

                result.Append(_characters[index]);
                gammaIndex++;

                if (gammaIndex + 1 == _gamma.Length) gammaIndex = 0;
            }

            return result.ToString();
        }

        private static IEnumerable<int> GenerateGamma(int n)
        {
            var used = new List<int>();
            var rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                var number = rnd.Next(0, 94);
                while (used.Contains(number)) number = rnd.Next(0, 94);

                yield return number;
                used.Add(number);
            }
        }

        private static int Mod(int x, int m) => (x % m + m) % m;
    }
}
