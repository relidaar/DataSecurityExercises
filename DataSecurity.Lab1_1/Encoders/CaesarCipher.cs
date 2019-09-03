using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_1.Encoders
{
    class CaesarCipher : IEncoder
    {
        public string EncoderName { get; }

        private readonly int _key;

        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public CaesarCipher(int key, string encoderName)
        {
            _key = key;
            EncoderName = encoderName;
        }

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
    }
}
