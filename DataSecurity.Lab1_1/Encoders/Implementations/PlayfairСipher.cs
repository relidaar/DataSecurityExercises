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
        private char[,] _matrix;

        public PlayfairСipher(string keyword) => _keyword = keyword;

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper().Replace("J", "I").Replace(" ", "");
            _keyword = _keyword.ToUpper().Replace(" ", "");
            _keyword = string.Join(string.Empty, _keyword.ToCharArray().Distinct());

            string result = "";
            string key = _keyword + string.Join("", Characters.Except(_keyword)).Replace("J", "");

            _matrix = CreateMatrix(key);

            var pairs = CreatePairs(message);

            foreach (var pair in pairs)
            {
                if (!Characters.Contains(pair[0]) || !Characters.Contains(pair[1])) continue;

                int[] first = GetCoordinates(pair[0], _matrix);
                int[] second = GetCoordinates(pair[1], _matrix);

                int x0 = first[0];
                int y0 = first[1];
                int x1 = second[0];
                int y1 = second[1];

                if (first[0] == second[0])
                {
                    result = GetInRow(_matrix, x0, y0, x1, y1, result);
                }
                else if (first[1] == second[1])
                {
                    result = GetInColumn(_matrix, x0, y0, x1, y1, result);
                }
                else
                {
                    result = GetInRect(_matrix, x0, y0, x1, y1, result);
                }
            }

            return result.Replace(" ", "");
        }

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            encryptedMessage = encryptedMessage.ToUpper().Replace("J", "I").Replace(" ", "");

            string result = "";

            var pairs = CreatePairs(encryptedMessage);

            foreach (var pair in pairs)
            {
                if (!Characters.Contains(pair[0]) || !Characters.Contains(pair[1])) continue;

                int[] first = GetCoordinates(pair[0], _matrix);
                int[] second = GetCoordinates(pair[1], _matrix);

                int x0 = first[0];
                int y0 = first[1];
                int x1 = second[0];
                int y1 = second[1];

                if (first[0] == second[0])
                {
                    result = GetInRowDecrypt(_matrix, x0, y0, x1, y1, result);
                }
                else if (first[1] == second[1])
                {
                    result = GetInColumnDecrypt(_matrix, x0, y0, x1, y1, result);
                }
                else
                {
                    result = GetInRect(_matrix, x0, y0, x1, y1, result);
                }
            }

            return result.Replace(" ", "");
        }

        private string GetInRect(char[,] matrix, int x0, int y0, int x1, int y1, string result)
        {
            result += matrix[x0, y1];
            result += matrix[x1, y0];

            return result;
        }

        private string GetInRow(char[,] matrix, int x0, int y0, int x1, int y1, string result)
        {
            result += y0 == 4 ? matrix[x0, 0] : matrix[x0, y0 + 1];
            result += y1 == 4 ? matrix[x1, 0] : matrix[x1, y1 + 1];

            return result;
        }

        private string GetInColumn(char[,] matrix, int x0, int y0, int x1, int y1, string result)
        {
            result += x0 == 4 ? matrix[0, y0] : matrix[x0 + 1, y0];
            result += x1 == 4 ? matrix[0, y1] : matrix[x1 + 1, y1];

            return result;
        }

        private string GetInRowDecrypt(char[,] matrix, int x0, int y0, int x1, int y1, string result)
        {
            result += y0 == 0 ? matrix[x0, 4] : matrix[x0, y0 - 1];
            result += y1 == 0 ? matrix[x1, 4] : matrix[x1, y1 - 1];

            return result;
        }

        private string GetInColumnDecrypt(char[,] matrix, int x0, int y0, int x1, int y1, string result)
        {
            result += x0 == 0 ? matrix[4, y0] : matrix[x0 - 1, y0];
            result += x1 == 0 ? matrix[4, y1] : matrix[x1 - 1, y1];

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
                if (!Characters.Contains(symbol)) continue;
                
                pair += symbol;
                if (pair.Length != 2) continue;

                pairs.Add(pair);
                pair = "";
            }

            return pairs;
        }
    }
}
