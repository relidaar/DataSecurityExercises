using System;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    internal class TrithemiusEncoder : IEncoder
    {
        private readonly int _a;
        private readonly int _b;
        private readonly int _c;
        private readonly string _characters;

        public TrithemiusEncoder(int a, int b, int c)
        {
            if (a < 0 || b < 0 || c < 0) throw new Exception("a, b or c cannot be less than zero");

            _a = a;
            _b = b;
            _c = c;
            _characters = string.Join("", Extensions.GenerateAlphabet(94, 32).ToArray());
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in message)
            {
                var index = (_characters.IndexOf(symbol) + GetShift(message.IndexOf(symbol)))
                    .Mod(_characters.Length);

                result.Append(_characters[index]);
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in encryptedMessage)
            {
                var index = (_characters.IndexOf(symbol) - GetShift(encryptedMessage.IndexOf(symbol)))
                    .Mod(_characters.Length);

                result.Append(_characters[index]);
            }

            return result.ToString();
        }

        private int GetShift(int p) => _a * p * p + _b * p + _c;
    }
}