using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    class RouteEncoder : IEncoder
    {
        private readonly int _numberOfColumns;
        private const char Placeholder = '*';

        public RouteEncoder(int numberOfColumns)
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

            var matrix = CreateMatrix(message, n, m);

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

            var result = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result.Append(matrix[i, j]);
                }
            }

            return result.ToString().Trim(Placeholder);
        }

        private static char[,] CreateMatrix(string input, int n, int m, bool transposed = false)
        {
            var matrix = new char[n, m];

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
    }
}
