using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    class DoubleTranspositionEncoder : IEncoder
    {
        private readonly int _numberOfColumns;

        private int[] _rows;
        private int[] _columns;
        private const char Placeholder = '*';

        public DoubleTranspositionEncoder(int numberOfColumns)
        {
            if (numberOfColumns <= 0) throw new Exception("Number of columns cannot be equal to or less than zero");

            _numberOfColumns = numberOfColumns;
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            message = message.Replace(" ", "");
            while (message.Length % _numberOfColumns != 0) message += Placeholder;

            int n = message.Length / _numberOfColumns;
            int m = _numberOfColumns;

            _rows = GeneratePermutations(n).ToArray();
            _columns = GeneratePermutations(m).ToArray();

            var matrix = CreateMatrix(message, n, m);

            matrix = DoPermutations(matrix, n, m);

            var result = new StringBuilder();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result.Append(matrix[j, i]);
                }
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            int n = encryptedMessage.Length / _numberOfColumns;
            int m = _numberOfColumns;

            var matrix = CreateMatrix(encryptedMessage, n, m, true);

            var transposed = DoReversePermutations(matrix, n, m);

            var result = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result.Append(transposed[i, j]);
                }
            }

            return result.ToString().Trim(Placeholder);
        }

        private char[,] DoPermutations(char[,] matrix, int n, int m)
        {
            var result = new char[n, m];

            for (var i = 0; i < m; i++)
            {
                var column = GetColumn(matrix, _columns[i]);
                for (int j = 0; j < n; j++)
                {
                    result[j, i] = column[j];
                }
            }

            var temp = (char[,]) result.Clone();

            for (var i = 0; i < _rows.Length; i++)
            {
                var row = GetRow(temp, _rows[i]);
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = row[j];
                }
            }

            return result;
        }

        private char[,] DoReversePermutations(char[,] matrix, int n, int m)
        {
            var result = new char[n, m];

            for (int i = 0; i < n; i++)
            {
                var index = Array.IndexOf(_rows, i);
                var row = GetRow(matrix, index);
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = row[j];
                }
            }

            var temp = (char[,])result.Clone();

            for (int i = 0; i < m; i++)
            {
                var index = Array.IndexOf(_columns, i);
                var column = GetColumn(temp, index);
                for (int j = 0; j < n; j++)
                {
                    result[j, i] = column[j];
                }
            }

            return result;
        }

        public char[] GetColumn(char[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }

        public char[] GetRow(char[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
        }

        private static char[,] CreateMatrix(string input, int n, int m, bool transposed = false)
        {
            var matrix = new char[n,m];

            int index = 0;
            if (transposed)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix[j, i] = input[index];
                        index++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        matrix[i, j] = input[index];
                        index++;
                    }
                }
            }

            return matrix;
        }

        private static IEnumerable<int> GeneratePermutations(int n)
        {
            var used = new List<int>();

            var rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                int num = rnd.Next(0, n);
                while (used.Contains(num)) num = rnd.Next(0, n);

                yield return num;
                used.Add(num);
            }
        }
    }
}
