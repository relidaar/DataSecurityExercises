using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_1
{
    internal class PlayfairEncoder : IEncoder
    {
        private readonly char[,] _matrix;
        private readonly string _characters;

        public PlayfairEncoder(string keyword)
        {
            if (keyword == null) throw new NullReferenceException();

            _characters = string.Join("", EncoderExtensions.GenerateAlphabet(26, 65).ToArray());

            keyword = keyword.ToUpper().Replace(" ", "");

            // Remove duplicates from keyword
            keyword = string.Join(string.Empty, keyword.ToCharArray().Distinct());

            // Create key from keyword and alphabet without duplicates
            var key = keyword + string.Join("", _characters.Except(keyword)).Replace("J", "");

            _matrix = CreateMatrix(key);

            _characters = string.Join("", EncoderExtensions.GenerateAlphabet(94, 32).ToArray());
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var pairs = CreatePairs(message.ToUpper().Replace("J", "I"));

            var result = new StringBuilder();
            foreach (var pair in pairs.Where(p => _characters.Contains(p[0]) && _characters.Contains(p[1])))
            {
                var first = GetCoordinates(pair[0]);
                var second = GetCoordinates(pair[1]);

                // Check rules
                if (first.x == second.x)
                    GetInRow(result, first, second);
                else if (first.y == second.y)
                    GetInColumn(result, first, second);
                else
                    GetInRect(result, first, second);
            }

            return result.ToString().Replace(" ", "");
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var pairs = CreatePairs(encryptedMessage.ToUpper().Replace("J", "I"));

            var result = new StringBuilder();
            foreach (var pair in pairs.Where(p => _characters.Contains(p[0]) && _characters.Contains(p[1])))
            {
                var first = GetCoordinates(pair[0]);
                var second = GetCoordinates(pair[1]);

                // Check rules
                if (first.x == second.x)
                    GetInRowDecrypt(result, first, second);
                else if (first.y == second.y)
                    GetInColumnDecrypt(result, first, second);
                else
                    GetInRect(result, first, second);
            }

            return result.ToString().Replace(" ", "");
        }

        private (int x, int y) GetCoordinates(char c)
        {
            for (var x = 0; x < 5; x++)
            for (var y = 0; y < 5; y++)
                if (_matrix[x, y] == c)
                    return (x, y);

            return (0, 0);
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

        private static IEnumerable<string> CreatePairs(string message)
        {
            for (var i = 1; i < message.Length; i++)
                if (message[i] == message[i - 1])
                    message = message.Insert(i, "X");

            if (message.Length % 2 != 0) message += "X";

            var pairs = new List<string>();
            for (var i = 0; i < message.Length; i += 2) pairs.Add(string.Concat(message[i], message[i + 1]));

            return pairs;
        }

        public void GetInRect(StringBuilder input, (int, int) first, (int, int) second)
        {
            var (x1, y1) = first;
            var (x2, y2) = second;

            input.Append(_matrix[x1, y2]);
            input.Append(_matrix[x2, y1]);
        }

        public void GetInRow(StringBuilder input, (int, int) first, (int, int) second)
        {
            var (x1, y1) = first;
            var (x2, y2) = second;

            input.Append(y1 == 4 ? _matrix[x1, 0] : _matrix[x1, y1 + 1]);
            input.Append(y2 == 4 ? _matrix[x2, 0] : _matrix[x2, y2 + 1]);
        }

        public void GetInColumn(StringBuilder input, (int, int) first, (int, int) second)
        {
            var (x1, y1) = first;
            var (x2, y2) = second;

            input.Append(x1 == 4 ? _matrix[0, y1] : _matrix[x1 + 1, y1]);
            input.Append(x2 == 4 ? _matrix[0, y2] : _matrix[x2 + 1, y2]);
        }

        public void GetInRowDecrypt(StringBuilder input, (int, int) first, (int, int) second)
        {
            var (x1, y1) = first;
            var (x2, y2) = second;

            input.Append(y1 == 0 ? _matrix[x1, 4] : _matrix[x1, y1 - 1]);
            input.Append(y2 == 0 ? _matrix[x2, 4] : _matrix[x2, y2 - 1]);
        }

        public void GetInColumnDecrypt(StringBuilder input, (int, int) first, (int, int) second)
        {
            var (x1, y1) = first;
            var (x2, y2) = second;

            input.Append(x1 == 0 ? _matrix[4, y1] : _matrix[x1 - 1, y1]);
            input.Append(x2 == 0 ? _matrix[4, y2] : _matrix[x2 - 1, y2]);
        }
    }
}