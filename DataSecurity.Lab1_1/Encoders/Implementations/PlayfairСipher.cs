using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class PlayfairСipher : BaseEncoder, IEncoder
    {
        public string Name => "Playfair cipher";

        private string _keyword;

        public PlayfairСipher(string keyword) => _keyword = keyword;

        public string Encrypt(string message)
        {
            if (message == null) return null;
            if (message.Contains("J")) message = message.Replace("J", "I");

            message = message.ToUpper();
            _keyword = _keyword.ToUpper();
            _keyword = string.Join(string.Empty, _keyword.ToCharArray().Distinct());

            string result = "";
            string key = _keyword + string.Join("", Characters.Except(_keyword)).Replace("J", "");

            var matrix = CreateMatrix(key);

            var pairs = CreatePairs(message);

            foreach (var pair in pairs)
            {
                if (!Characters.Contains(pair[0]) || !Characters.Contains(pair[1])) continue;

                int[] first = GetCoordinates(pair[0], matrix);
                int[] second = GetCoordinates(pair[1], matrix);

                if (first[0] == second[0])
                {
                    result = GetInRow(matrix, first, second, result);
                }
                else if (first[1] == second[1])
                {
                    result = GetInColumn(matrix, first, second, result);
                }
                else
                {
                    result = GetInRect(matrix, first, second, result);

                }
            }

            return result.Replace(" ", "");
        }

        private string GetInRect(char[,] matrix, int[] first, int[] second, string result)
        {
            int x0 = first[0];
            int y0 = first[1];
            int x1 = second[0];
            int y1 = second[1];

            if (y0 < y1)
            {
                result += matrix[x0, y1] + matrix[x1, y0];
            }
            else
            {
                result += matrix[x1, y0] + matrix[x0, y1];
            }

            return result;
        }

        private string GetInRow(char[,] matrix, int[] first, int[] second, string result)
        {
            int x0 = first[0];
            int y0 = first[1];
            int x1 = second[0];
            int y1 = second[1];

            if (y0 == 4)
            {
                result += matrix[x0, 0] + matrix[x1, y1 + 1];
            }

            if (y1 == 4)
            {
                result += matrix[x1, 0] + matrix[x0, y0 + 1];
            }

            return result;
        }

        private string GetInColumn(char[,] matrix, int[] first, int[] second, string result)
        {
            int x0 = first[0];
            int y0 = first[1];
            int x1 = second[0];
            int y1 = second[1];

            if (x0 == 4)
            {
                result += matrix[0, y0] + matrix[x1 + 1, y1];
            }

            if (x1 == 4)
            {
                result += matrix[0, y1] + matrix[x0 + 1, y0];
            }

            return result;
        }

        private int[] GetCoordinates(char c, char[,] matrix)
        {
            var coordinates = new int[2];
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (matrix[x, y] == c) coordinates = new[] { x, y };
                }
            }

            return coordinates;
        }

        private char[,] CreateMatrix(string key)
        {
            var matrix = new char[5, 5];
            int current = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = key[current];
                    current++;
                }
            }

            return matrix;
        }

        private IEnumerable<string> CreatePairs(string message)
        {
            var pairs = new List<string>();

            for (var i = 1; i < message.Length; i++)
            {
                if (message[i] == message[i - 1]) message = message.Insert(i, "X");
            }

            if (message.Length % 2 != 0) message += "X";

            string pair = "";
            foreach (var symbol in message)
            {
                pair += symbol;
                if (pair.Length != 2) continue;

                pairs.Add(pair);
                pair = "";
            }

            return pairs;
        }
    }
}
