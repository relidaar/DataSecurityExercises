using System.Linq;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class SloganCipher : BaseEncoder, IEncoder
    {
        public string Name => "Slogan cipher";

        private string _keyword;

        public SloganCipher(string keyword) => _keyword = keyword;

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper().Replace(" ", "");
            _keyword = _keyword.ToUpper().Replace(" ", "");
            _keyword = string.Join(string.Empty, _keyword.ToCharArray().Distinct());

            string result = "";
            string key = _keyword + string.Join(string.Empty, Characters.Except(_keyword));

            foreach (var symbol in message)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = Characters.IndexOf(symbol);
                result += key[index];
            }

            return result;
        }

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            encryptedMessage = encryptedMessage.ToUpper().Replace(" ", "");
            _keyword = _keyword.ToUpper().Replace(" ", "");
            _keyword = string.Join(string.Empty, _keyword.ToCharArray().Distinct());

            string result = "";
            string key = _keyword + string.Join(string.Empty, Characters.Except(_keyword));

            foreach (var symbol in encryptedMessage)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = key.IndexOf(symbol);
                result += Characters[index];
            }

            return result;
        }
    }
}
