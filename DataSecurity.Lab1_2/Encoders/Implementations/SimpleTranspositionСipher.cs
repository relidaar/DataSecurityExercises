using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    class SimpleTranspositionСipher : IEncoder
    {
        private int[,] _key;
        public string Name => "Simple transposition cipher";

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper().Replace(" ", "");

            int n = message.Length;
            CreateKey(n);
            var result = new char[n];

            for (int i = 0; i < n; i++)
            {
                result[_key[1, i]] = message[_key[0, i]];
            }

            return string.Concat(result);
        }

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null || _key == null) return null;

            encryptedMessage = encryptedMessage.ToUpper().Replace(" ", "");
            int n = encryptedMessage.Length;
            var result = new char[n];

            for (int i = 0; i < n; i++)
            {
                result[_key[0, i]] = encryptedMessage[_key[1, i]];
            }

            return string.Concat(result);
        }

        private void CreateKey(int n)
        {
            var used = new List<int>();
            _key = new int[2,n];

            var rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                int pos = rnd.Next(0, n);
                while (used.Contains(pos)) pos = rnd.Next(0, n);

                _key[0, i] = i;
                _key[1, i] = pos;
                used.Add(pos);
            }
        }
    }
}
