using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class VigenereCipher : BaseEncoder, IEncoder
    {
        public string Name => "Vigenere cipher";

        private string _keyword;

        public VigenereCipher(string keyword) => _keyword = keyword;

        public string Encrypt(string message)
        {
            if (message == null) return null;

            _keyword = _keyword.ToUpper().Replace(" ", "");
            message = message.ToUpper();

            string result = "";
            int keywordIndex = 0;

            foreach (var symbol in message)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = (Characters.IndexOf(symbol) + Characters.IndexOf(_keyword[keywordIndex]))
                            % Characters.Length;

                result += Characters[index];
                keywordIndex++;

                if (keywordIndex + 1 == _keyword.Length) keywordIndex = 0;
            }

            return result;
        }
    }
}
