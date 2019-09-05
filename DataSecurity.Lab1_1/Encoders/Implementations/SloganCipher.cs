using System.Linq;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class SloganCipher : BaseEncoder, IEncoder
    {
        public string Name => "Slogan cipher";

        private string _keyword;

        public SloganCipher(string keyword = "secret") => _keyword = keyword;

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper();
            _keyword = _keyword.ToUpper();
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
    }
}
