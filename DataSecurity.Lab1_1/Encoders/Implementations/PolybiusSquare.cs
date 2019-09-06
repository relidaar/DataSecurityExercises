using System.Linq;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class PolybiusSquare : BaseEncoder, IEncoder
    {
        public string Name => "Polybius square";

        private string _keyword;

        public PolybiusSquare(string keyword) => _keyword = keyword;

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper().Replace("J", "I").Replace(" ", "");
            _keyword = _keyword.ToUpper().Replace(" ", "");
            _keyword = string.Join(string.Empty, _keyword.ToCharArray().Distinct());

            string result = "";
            string key = _keyword + string.Join("", Characters.Except(_keyword)).Replace("J", "");

            foreach (var letter in message)
            {
                if (!Characters.Contains(letter)) continue;

                int column = key.IndexOf(letter) % 5 + 1;
                int row = key.IndexOf(letter) / 5 + 1;

                result += $"{row}{column} ";
            }

            return result;
        }
    }
}
