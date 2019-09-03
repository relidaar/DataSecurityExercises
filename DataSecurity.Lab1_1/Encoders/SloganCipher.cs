using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSecurity.Lab1_1.Encoders
{
    class SloganCipher : BaseEncoder, IEncoder
    {
        public string EncoderName => "Slogan cipher";

        private string _key;
        private readonly string _keyword;

        public SloganCipher(string keyword) => _keyword = keyword;

        public string Encrypt(string message)
        {
            string encryptedMessage = "";
            if (message == null) return encryptedMessage;

            foreach (var letter in message)
            {
                if (!Alphabet.Contains(letter)) continue;

                int position = Alphabet.IndexOf(letter);
                encryptedMessage += _key[position];
            }

            return encryptedMessage;
        }

        public void GenerateKey()
        {
            _key = string.Join("", _keyword.ToUpper()
                .Replace(" ", "").ToCharArray().Distinct());

            _key += string.Join("", Alphabet.Except(_key));
        }
    }
}
