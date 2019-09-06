using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class CaesarCipher : BaseEncoder, IEncoder
    {
        public string Name => "Caesar cipher";

        private readonly int _shift;

        public CaesarCipher(int shift) => _shift = shift;

        public string Encrypt(string message)
        {
            if (_shift < 0 || _shift > Characters.Length - 1) return null;
            if (message == null) return null;

            string result = "";

            foreach (var symbol in message)
            {
                int index = Characters.IndexOf(symbol) + _shift;

                if (index > Characters.Length - 1) index -= Characters.Length;

                result += Characters[index];
            }

            return result;
        }

        public void GenerateKey() {}
    }
}
