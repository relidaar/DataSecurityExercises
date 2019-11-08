using System;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Implementations.Lab1_1
{
    internal class CaesarEncoder : IEncoder
    {
        private readonly int _shift;
        private readonly string _characters;

        public CaesarEncoder(int shift)
        {
            if (shift < 0) throw new Exception("Shift cannot be less than zero");
            _shift = shift;
            _characters = string.Join("", EncoderExtensions.GenerateAlphabet(94, 32).ToArray());
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in message)
            {
                var index = (_characters.IndexOf(symbol) + _shift).Mod(_characters.Length);
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
                var index = (_characters.IndexOf(symbol) - _shift).Mod(_characters.Length);
                result.Append(_characters[index]);
            }

            return result.ToString();
        }
    }
}