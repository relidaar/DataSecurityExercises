using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_1.Encoders
{
    class VigenereCipher : BaseEncoder, IEncoder
    {
        public string EncoderName => "Vigenere cipher";

        private string _keyword;

        public VigenereCipher(string keyword) => _keyword = keyword;

        public string Encrypt(string message)
        {
            string result = "";
            if (message == null) return result;

            int keywordIndex = 0;
            foreach (var letter in message)
            {
                int c = (Array.IndexOf(Alphabet.ToCharArray(), letter) +
                         Array.IndexOf(Alphabet.ToCharArray(), _keyword[keywordIndex])) % Alphabet.Length;

                result += Alphabet[c];
                keywordIndex++;

                if (keywordIndex + 1 == _keyword.Length) keywordIndex = 0;
            }

            return result;
        }

        public void GenerateKey() { }
    }
}
