using System;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    internal class TrithemiusEncoder : BaseEncoder, IEncoder
    {
        private readonly int _a;
        private readonly int _b;
        private readonly int _c;

        public TrithemiusEncoder(int a, int b, int c)
        {
            if (a < 0 || b < 0 || c < 0) throw new Exception("a, b or c cannot be less than zero");

            _a = a;
            _b = b;
            _c = c;
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in message.ToUpper().Where(c => Characters.Contains(c)))
            {
                var index = (Characters.IndexOf(symbol) + GetShift(message.IndexOf(symbol)))
                    .Mod(Characters.Length);

                result.Append(Characters[index]);
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in encryptedMessage.ToUpper().Where(c => Characters.Contains(c)))
            {
                var index = (Characters.IndexOf(symbol) - GetShift(encryptedMessage.IndexOf(symbol)))
                    .Mod(Characters.Length);

                result.Append(Characters[index]);
            }

            return result.ToString();
        }

        private int GetShift(int p)
        {
            return _a * p * p + _b * p + _c;
        }
    }
}