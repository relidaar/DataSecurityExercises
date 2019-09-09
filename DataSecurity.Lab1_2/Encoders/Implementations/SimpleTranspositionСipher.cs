using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    class SimpleTranspositionСipher : BaseEncoder, IEncoder
    {
        private List<(int, int)> _key;
        public string Name => "Simple transposition cipher";

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper().Replace(" ", "");

            CreateKey(message.Length);
            var result = new char[message.Length];

            foreach (var valueTuple in _key)
            {
                result[valueTuple.Item2] = message[valueTuple.Item1];
            }

            return string.Concat(result);
        }

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null || _key == null) return null;

            encryptedMessage = encryptedMessage.ToUpper().Replace(" ", "");
            int n = encryptedMessage.Length;
            var result = new char[n];

            foreach (var valueTuple in _key)
            {
                result[valueTuple.Item1] = encryptedMessage[valueTuple.Item2];
            }

            return string.Concat(result);
        }

        private void CreateKey(int messageLength)
        {
            var used = new List<int>();
            _key = new List<(int, int)>();

            var rnd = new Random();
            for (int i = 0; i < messageLength; i++)
            {
                int pos = rnd.Next(0, messageLength);
                while (used.Contains(pos)) pos = rnd.Next(0, messageLength);

                _key.Add((i, pos));
                used.Add(pos);
            }
        }
    }
}
