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
            message = message.ToUpper().Replace(" ", "");

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

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            _keyword = _keyword.ToUpper().Replace(" ", "");
            encryptedMessage = encryptedMessage.ToUpper().Replace(" ", "");

            string result = "";
            int keywordIndex = 0;

            foreach (var symbol in encryptedMessage)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = Mod(
                    Characters.IndexOf(symbol) + Characters.Length - Characters.IndexOf(_keyword[keywordIndex]),
                    Characters.Length);

                result += Characters[index];
                keywordIndex++;

                if (keywordIndex + 1 == _keyword.Length) keywordIndex = 0;
            }

            return result;
        }

        private int Mod(int x, int m) => (x % m + m) % m;
    }
}
