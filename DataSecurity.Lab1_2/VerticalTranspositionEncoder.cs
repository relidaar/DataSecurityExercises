using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_2
{
    public class VerticalTranspositionEncoder : IEncoder
    {
        private const char Placeholder = '*';
        private readonly int[] _key;

        public VerticalTranspositionEncoder(string keyword)
        {
            keyword = keyword.Replace(" ", "");

            // Remove duplicates from keyword
            keyword = string.Join(string.Empty, keyword.ToCharArray().Distinct());

            _key = CreateKey(keyword).ToArray();
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            message = message.Replace(" ", "");
            while (message.Length % _key.Length != 0) message += Placeholder;

            var n = message.Length / _key.Length;
            var m = _key.Length;

            var matrix = new char[n, m];
            var index = 0;
            for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
            {
                matrix[i, j] = message[index];
                index++;
            }

            var result = new StringBuilder();
            foreach (var i in _key)
                for (var j = 0; j < n; j++)
                    result.Append(matrix[j, i]);

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var n = encryptedMessage.Length / _key.Length;
            var m = _key.Length;

            var matrix = new char[n, m];
            var index = 0;
            foreach (var i in _key)
                for (var j = 0; j < n; j++)
                {
                    matrix[j, i] = encryptedMessage[index];
                    index++;
                }

            var result = new StringBuilder();
            for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
                result.Append(matrix[i, j]);

            return result.ToString().Trim(Placeholder);
        }

        private static IEnumerable<int> CreateKey(string keyword)
        {
            var sorted = keyword.ToArray();
            Array.Sort(sorted);

            foreach (var c in sorted) yield return keyword.IndexOf(c);
        }
    }
}