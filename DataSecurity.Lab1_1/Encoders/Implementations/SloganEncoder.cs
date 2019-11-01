using System;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    internal class SloganEncoder : IEncoder
    {
        private readonly string _key;
        private readonly string _characters;

        public SloganEncoder(string keyword)
        {
            if (keyword == null) throw new NullReferenceException();

            _characters = string.Join("", Extensions.GenerateAlphabet(94, 32).ToArray());

            // Remove duplicates from keyword
            keyword = string.Join(string.Empty, keyword.ToCharArray().Distinct());

            // Create key from keyword and alphabet without duplicates
            _key = keyword + string.Join(string.Empty, _characters.Except(keyword));
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in message)
            {
                var index = _characters.IndexOf(symbol);
                result.Append(_key[index]);
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in encryptedMessage)
            {
                var index = _key.IndexOf(symbol);
                result.Append(_characters[index]);
            }

            return result.ToString();
        }
    }
}