using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_3.Encoders.Interfaces;

namespace DataSecurity.Lab1_3.Encoders.Implementations
{
    class GammaCipher : BaseEncoder, IEncoder
    {
        public string Name => "Gamma cipher";

        private readonly int _gammaCount;
        private int[] _gamma;

        public GammaCipher(int gammaCount) => _gammaCount = gammaCount;

        public string Encrypt(string message)
        {
            if (_gammaCount <= 0) return null;
            if (message == null) return null;

            _gamma = GenerateGamma();

            message = message.ToUpper().Replace(" ", "");

            string result = "";
            int gammaIndex = 0;

            foreach (var symbol in message)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = (Characters.IndexOf(symbol) + _gamma[gammaIndex]) % Characters.Length;

                result += Characters[index];
                gammaIndex++;

                if (gammaIndex + 1 == _gamma.Length) gammaIndex = 0;
            }

            return result;
        }

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            encryptedMessage = encryptedMessage.ToUpper().Replace(" ", "");

            string result = "";
            int gammaIndex = 0;

            foreach (var symbol in encryptedMessage)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = Mod(Characters.IndexOf(symbol) + Characters.Length - _gamma[gammaIndex], 
                    Characters.Length);

                result += Characters[index];
                gammaIndex++;

                if (gammaIndex + 1 == _gamma.Length) gammaIndex = 0;
            }

            return result;
        }

        private int[] GenerateGamma()
        {
            var used = new List<int>();
            var gamma = new int[_gammaCount];
            var rnd = new Random();
            for (int i = 0; i < _gammaCount; i++)
            {
                var number = rnd.Next(0, 26);
                while (used.Contains(number)) number = rnd.Next(0, 26);

                gamma[i] = number;
                used.Add(number);
            }

            return gamma;
        }

        private int Mod(int x, int m) => (x % m + m) % m;
    }
}
