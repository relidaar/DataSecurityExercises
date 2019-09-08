using System;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class CaesarCipher : BaseEncoder, IEncoder
    {
        public string Name => "Caesar cipher";

        private readonly int _shift;

        public CaesarCipher(int shift) => _shift = shift;

        public string Encrypt(string message)
        {
            if (_shift < 0 || _shift > Characters.Length - 1) return null;
            if (message == null) return null;

            string result = "";

            foreach (var symbol in message)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = (Characters.IndexOf(symbol) + _shift) % Characters.Length;

                result += Characters[index];
            }

            return result;
        }

        public string Decrypt(string encryptedMessage)
        {
            if (_shift < 0 || _shift > Characters.Length - 1) return null;
            if (encryptedMessage == null) return null;

            string result = "";

            foreach (var symbol in encryptedMessage)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = Mod(Characters.IndexOf(symbol) - _shift, Characters.Length);

                result += Characters[index];
            }

            return result;
        }

        private int Mod(int x, int m) => (x % m + m) % m;
    }
}
