using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class TrithemiusCipher : BaseEncoder, IEncoder
    {
        public string Name => "Trithemius cipher";

        private readonly int _a;
        private readonly int _b;
        private readonly int _c;

        public TrithemiusCipher(int a, int b, int c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public string Encrypt(string message)
        {
            if (_a < 0 || _b < 0 || _c < 0) return null;
            if (message == null) return null;

            message = message.ToUpper().Replace(" ", "");
            string result = "";

            foreach (var symbol in message)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = (Characters.IndexOf(symbol) + GetShift(message.IndexOf(symbol))) % Characters.Length;

                result += Characters[index];
            }

            return result;
        }

        private int GetShift(int p) => _a * p * p + _b * p + _c;
    }
}
