using System;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_1
{
    internal class PolybiusEncoder : IEncoder
    {
        private readonly char[,] _matrix;
        private readonly string _characters;

        public PolybiusEncoder(string keyword)
        {
            if (keyword == null) throw new NullReferenceException();

            _characters = string.Join("", EncoderExtensions.GenerateAlphabet(26, 65).ToArray());

            keyword = keyword.ToUpper().Replace("J", "I").Replace(" ", "");

            // Remove duplicates from keyword
            keyword = string.Join(string.Empty, keyword.ToCharArray().Distinct());

            // Create key from keyword and alphabet without duplicates
            var key = keyword + string.Join("", _characters.Except(keyword)).Replace("J", "");

            _matrix = CreateMatrix(key);
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var symbol in message.ToUpper().Replace("J", "I").Where(c => _characters.Contains(c)))
                for (var i = 0; i < 5; i++)
                for (var j = 0; j < 5; j++)
                {
                    // Find indices of current symbol in key matrix
                    if (_matrix[i, j] != symbol) continue;

                    result.Append($"{i}{j} ");
                }

            return result.ToString().Trim();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            var result = new StringBuilder();
            foreach (var indices in encryptedMessage.Split(' '))
            {
                var row = indices[0] - '0';
                var column = indices[1] - '0';

                result.Append(_matrix[row, column]);
            }

            return result.ToString();
        }

        private static char[,] CreateMatrix(string key)
        {
            var matrix = new char[5, 5];
            var current = 0;
            for (var i = 0; i < 5; i++)
            for (var j = 0; j < 5; j++)
            {
                matrix[i, j] = key[current];
                current++;
            }

            return matrix;
        }
    }
}