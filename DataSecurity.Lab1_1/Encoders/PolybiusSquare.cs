using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSecurity.Lab1_1.Encoders
{
    class PolybiusSquare : BaseEncoder, IEncoder
    {
        public string EncoderName => "Polybius square";

        private string _key;
        private readonly string _keyword;

        public PolybiusSquare(string keyword = null) => _keyword = keyword;

        public string Encrypt(string message)
        {
            string encryptedMessage = "";
            if (message == null) return encryptedMessage;

            if (message.Contains("J")) message = message.Replace("J", "I");

            foreach (var letter in message)
            {
                if (!Alphabet.Contains(letter)) continue;

                int row = _key.IndexOf(letter) % 5 + 1;
                int column = _key.IndexOf(letter) / 5 + 1;

                encryptedMessage += $"{column}{row} ";
            }

            return encryptedMessage;
        }

        public void GenerateKey()
        {
            if (_keyword != null)
            {
                _key = string.Join("", _keyword.ToUpper()
                    .Replace(" ", "").ToCharArray().Distinct());

                _key += string.Join("", Alphabet.Except(_key));
            }
            else
            {
                _key = Alphabet;
            }

            _key = _key.Replace("J", "");
        }
    }
}
