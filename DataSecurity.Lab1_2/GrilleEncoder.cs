using System;
using System.Linq;
using System.Text;
using DataSecurity.Interfaces;
using static System.String;

namespace DataSecurity.Lab1_2
{
    internal class GrilleEncoder : IEncoder
    {
        private const char Placeholder = '*';

        private static readonly int[,] BaseGrille =
        {
            {1, 2, 3, 1},
            {3, 4, 4, 2},
            {2, 4, 4, 3},
            {1, 3, 2, 1}
        };

        private readonly int[,] _indices;

        public GrilleEncoder()
        {
            _indices = GetRotatingIndexes();
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            message = message.Trim(' ').Replace(" ", "");
            while ((int) (message.Length % Math.Pow(4, 2)) != 0) message += Placeholder;

            var result = new StringBuilder();

            var target = new char[4, 4];
            while (!IsNullOrEmpty(message))
            {
                for (var n = 0; n < 4; n++)
                {
                    for (var i = 0; i < 4; i++)
                    {
                        target[_indices[i, 0], _indices[i, 1]] = message.First();
                        message = message.Remove(0, 1);
                    }

                    target = RotateRight(target);
                }

                foreach (var item in target) result.Append(item);
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var result = new StringBuilder();

            while (!IsNullOrEmpty(encryptedMessage))
            {
                var original = new char[4, 4];

                for (var i = 0; i < 4; i++)
                for (var j = 0; j < 4; j++)
                {
                    original[i, j] = encryptedMessage.First();
                    encryptedMessage = encryptedMessage.Remove(0, 1);
                }

                for (var n = 0; n < 4; n++)
                {
                    for (var i = 0; i < 4; i++) result.Append(original[_indices[i, 0], _indices[i, 1]]);

                    original = RotateRight(original);
                }
            }

            return result.ToString().Trim(Placeholder);
        }

        private static int[,] GetRotatingIndexes()
        {
            var indexes = new int[4, 2];
            var matrix = BaseGrille;

            var r = new Random();
            for (var i = 0; i < 4; i++)
                while (true)
                {
                    var x = r.Next(0, 4 - 1);
                    var y = r.Next(0, 4 - 1);

                    if (matrix[x, y] != i + 1) continue;

                    indexes[i, 0] = x;
                    indexes[i, 1] = y;
                    break;
                }

            return indexes;
        }

        private static char[,] RotateRight(char[,] matrix)
        {
            var max = matrix.GetLength(0);
            var arr = new char[max, max];

            for (var i = 0; i < max; i++)
            for (var j = 0; j < max; j++)
                arr[j, max - i - 1] = matrix[i, j];

            return arr;
        }
    }
}