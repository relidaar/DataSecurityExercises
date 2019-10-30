using System;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    internal class VigenereEncoder : BaseEncoder, IEncoder
    {
        private readonly string _keyword;

        public VigenereEncoder(string keyword)
        {
            if (keyword == null) throw new NullReferenceException();

            _keyword = keyword.ToUpper().Replace(" ", "");
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var keywordIndex = 0;

            var result = new StringBuilder();
            foreach (var symbol in message.ToUpper().Where(c => Characters.Contains(c)))
            {
                var index = (Characters.IndexOf(symbol) + Characters.IndexOf(_keyword[keywordIndex]))
                    .Mod(Characters.Length);

                result.Append(Characters[index]);
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
            foreach (var symbol in encryptedMessage.ToUpper().Where(c => Characters.Contains(c)))
            {
                var index = (Characters.IndexOf(symbol) - Characters.IndexOf(_keyword[keywordIndex]))
                    .Mod(Characters.Length);

                result.Append(Characters[index]);
                keywordIndex++;

                if (keywordIndex + 1 == _keyword.Length) keywordIndex = 0;
            }

            return result.ToString();
        }
    }
}