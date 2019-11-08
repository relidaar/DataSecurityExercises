using System;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_1
{
    internal class VigenereEncoder : IEncoder
    {
        private readonly string _characters;
        private readonly string _keyword;

        public VigenereEncoder(string keyword)
        {
            _keyword = keyword ?? throw new NullReferenceException();
            _characters = string.Join("", EncoderExtensions.GenerateAlphabet(94, 32).ToArray());
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var keywordIndex = 0;

            var result = new StringBuilder();
            foreach (var symbol in message)
            {
                var index = (_characters.IndexOf(symbol) + _characters.IndexOf(_keyword[keywordIndex]))
                    .Mod(_characters.Length);

                result.Append(_characters[index]);
                keywordIndex++;

                if (keywordIndex + 1 == _keyword.Length) keywordIndex = 0;
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var keywordIndex = 0;

            var result = new StringBuilder();
            foreach (var symbol in encryptedMessage)
            {
                var index = (_characters.IndexOf(symbol) - _characters.IndexOf(_keyword[keywordIndex]))
                    .Mod(_characters.Length);

                result.Append(_characters[index]);
                keywordIndex++;

                if (keywordIndex + 1 == _keyword.Length) keywordIndex = 0;
            }

            return result.ToString();
        }
    }
}