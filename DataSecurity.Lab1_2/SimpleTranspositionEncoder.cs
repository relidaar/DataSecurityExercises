using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_2
{
    internal class SimpleTranspositionEncoder : IEncoder
    {
        private int[] _key;

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            message = message.Replace(" ", "");

            _key = CreateKey(message.Length).ToArray();

            var result = new StringBuilder();
            foreach (var i in _key) result.Append(message[i]);

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return null;

            var result = new StringBuilder();
            for (var i = 0; i < encryptedMessage.Length; i++)
            {
                var index = Array.IndexOf(_key, i);
                result.Append(encryptedMessage[index]);
            }

            return result.ToString();
        }

        private static IEnumerable<int> CreateKey(int n)
        {
            var used = new List<int>();

            var rnd = new Random();

            // Generate random positions
            for (var i = 0; i < n; i++)
            {
                var pos = rnd.Next(0, n);
                while (used.Contains(pos)) pos = rnd.Next(0, n);

                yield return pos;
                used.Add(pos);
            }
        }
    }
}