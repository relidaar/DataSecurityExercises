using System;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    internal class CaesarEncoder : BaseEncoder, IEncoder
    {
        private readonly int _shift;

        public CaesarEncoder(int shift)
        {
            if (shift < 0) throw new Exception("Shift cannot be less than zero");
            _shift = shift;
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in message.ToUpper().Where(c => Characters.Contains(c)))
            {
                var index = (Characters.IndexOf(symbol) + _shift).Mod(Characters.Length);
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
                var index = (Characters.IndexOf(symbol) - _shift).Mod(Characters.Length);
                result.Append(Characters[index]);
            }

            return result.ToString();
        }
    }
}