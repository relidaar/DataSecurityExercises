using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_2.Encoders.Interfaces;
using static System.String;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    class GrilleСipher : IEncoder
    {
        public string Name => "Grille cipher";

        private readonly Key _key;

        private static char _placeholder = '*';

        public GrilleСipher(Key key) => _key = key;

        public string Encrypt(string message)
        {
            if (IsNullOrEmpty(message)) return null;

            message = message.Trim(' ').Replace(" ", "");
            while (message.Length % Math.Pow((int)_key.Size, 2) != 0) message = Concat(message, _placeholder);

            var result = new StringBuilder();

            while (!IsNullOrEmpty(message))
            {
                char[,] target = new char[(int)_key.Size, (int)_key.Size];
                for (int n = 0; n < 4; n++)
                {
                    for (int i = 0; i < CardanGrille.Const(_key.Size); i++)
                    {
                        target[_key.Indices[i, 0], _key.Indices[i, 1]] = message.First();
                        message = message.Remove(0, 1);
                    }
                    target = target.RotateRight();
                }

                foreach (var item in target)
                    result.Append(item);
            }

            return result.ToString();
        }

        public string Decrypt(string encryptedMessage)
        {
            if (IsNullOrEmpty(encryptedMessage)) return null;

            if (encryptedMessage.Length % Math.Pow((int) _key.Size, 2) != 0) return null;

            var original = new char[(int)_key.Size, (int)_key.Size];
            var result = new StringBuilder();

            while (!IsNullOrEmpty(encryptedMessage))
            {
                original = new char[(int)_key.Size, (int)_key.Size];

                for (int i = 0; i < (int)_key.Size; i++)
                {
                    for (int j = 0; j < (int)_key.Size; j++)
                    {
                        original[i, j] = encryptedMessage.First();
                        encryptedMessage = encryptedMessage.Remove(0, 1);
                    }
                }

                for (int n = 0; n < 4; n++)
                {
                    for (int i = 0; i < CardanGrille.Const(_key.Size); i++)
                    {
                        result.Append(original[_key.Indices[i, 0], _key.Indices[i, 1]]);
                    }
                    original = original.RotateRight();
                }
            }

            return result.ToString().Trim(_placeholder);
        }
    }

    public class Key
    {
        public int[,] Indices { get; }
        public GrilleSize Size { get; }

        public Key(GrilleSize size)
        {
            Indices = CardanGrille.GetRotatingIndexes(size);
            Size = size;
        }
    }

    public enum GrilleSize
    {
        Four = 4,
        Five,
        Six
    }

    public static class CardanGrille
    {
        private static int[,] Base4
        {
            get
            {
                return new int[4, 4] {
                    { 1, 2, 3, 1 },
                    { 3, 4, 4, 2 },
                    { 2, 4, 4, 3 },
                    { 1, 3, 2, 1 } };
            }
        }

        private static int[,] Base5
        {
            get
            {
                return new int[5, 5] {
                    { 1, 2, 3, 4, 1 },
                    { 4, 5, 6, 5, 2 },
                    { 3, 6, 7, 6, 3 },
                    { 2, 5, 6, 5, 4 },
                    { 1, 4, 3, 2, 1 } };
            }
        }

        private static int[,] Base6
        {
            get
            {
                return new int[6, 6]
                {
                    {1,2,3,4,5,1 },
                    {5,6,7,8,6,2 },
                    {4,8,9,9,7,3 },
                    {3,7,9,9,8,4 },
                    {2,6,8,7,6,5 },
                    {1,5,4,3,2,1 }
                };
            }
        }

        public static int[,] GetGrille(GrilleSize b)
        {
            switch (b)
            {
                case GrilleSize.Four:
                    return Base4;
                case GrilleSize.Five:
                    return Base5;
                case GrilleSize.Six:
                    return Base6;
                default:
                    return null;
            }
        }

        public static int Const(GrilleSize b)
        {
            switch (b)
            {
                case GrilleSize.Four:
                    return 4;
                case GrilleSize.Five:
                    return 7;
                case GrilleSize.Six:
                    return 9;
                default:
                    return 0;
            }
        }

        public static int[,] GetRotatingIndexes(GrilleSize b)
        {
            int max = Const(b);
            int[,] indexes = new int[max, 2];
            int[,] matrix = GetGrille(b);

            for (int i = 0; i < max; i++)
            {
                int x, y;
                while (true)
                {
                    Random r = new Random();
                    x = r.Next(0, (int)b - 1);
                    y = r.Next(0, (int)b - 1);
                    if (matrix[x, y] == i + 1)
                    {
                        indexes[i, 0] = x;
                        indexes[i, 1] = y;
                        break;
                    }
                }
            }

            return indexes;
        }

        public static char[,] RotateRight(this char[,] matrix)
        {
            int max = matrix.GetLength(0);
            char[,] arr = new char[max, max];

            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    arr[j, max - i - 1] = matrix[i, j];
                }
            }
            return arr;
        }

        public static Key CreateKey(GrilleSize b) => new Key(b);
    }
}
