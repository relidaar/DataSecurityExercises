using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_1.Encoders
{
    class CaesarCipher : BaseEncoder, IEncoder
    {
        public string EncoderName => "Caesar cipher";

        private readonly int _key;

        public CaesarCipher(int key) => _key = key;

        public string Encrypt(string message)
        {
            string encryptedMessage = "";
            if (message == null) return encryptedMessage;

            foreach (var letter in message)
            {
                if (!Alphabet.Contains(letter)) continue;

                int position = Alphabet.IndexOf(letter) + _key;

                if (position < 0) position += Alphabet.Length;

                if (position > Alphabet.Length - 1) position -= Alphabet.Length;

                encryptedMessage += Alphabet[position];
            }

            return encryptedMessage;
        }

        public void GenerateKey() { }
    }
}
