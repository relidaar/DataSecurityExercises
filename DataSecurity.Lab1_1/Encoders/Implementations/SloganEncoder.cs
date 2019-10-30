using System;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    internal class SloganEncoder : BaseEncoder, IEncoder
    {
        private readonly string _key;

        public SloganEncoder(string keyword)
        {
            if (keyword == null) throw new NullReferenceException();

            keyword = keyword.ToUpper().Replace(" ", "");

            // Remove duplicates from keyword
            keyword = string.Join(string.Empty, keyword.ToCharArray().Distinct());

            // Create key from keyword and alphabet without duplicates
            _key = keyword + string.Join(string.Empty, Characters.Except(keyword));
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in message.ToUpper().Where(c => Characters.Contains(c)))
            {
                var index = Characters.IndexOf(symbol);
                result.Append(_key[index]);
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in encryptedMessage.ToUpper().Where(c => Characters.Contains(c)))
            {
                var index = _key.IndexOf(symbol);
                result.Append(Characters[index]);
            }

            return result.ToString();
        }
    }
}