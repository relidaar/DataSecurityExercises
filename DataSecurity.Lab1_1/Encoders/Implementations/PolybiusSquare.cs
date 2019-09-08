using System.Linq;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class PolybiusSquare : BaseEncoder, IEncoder
    {
        public string Name => "Polybius square";

        private string _keyword;

        public PolybiusSquare(string keyword) => _keyword = keyword;

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper().Replace("J", "I").Replace(" ", "");
            _keyword = _keyword.ToUpper().Replace(" ", "");
            _keyword = string.Join(string.Empty, _keyword.ToCharArray().Distinct());

            string result = "";
            string key = _keyword + string.Join("", Characters.Except(_keyword)).Replace("J", "");

            var matrix = CreateMatrix(key);

            foreach (var symbol in message)
            {
                if (!Characters.Contains(symbol)) continue;

                int column = 0;
                int row = 0;

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (matrix[i, j] == symbol)
                        {
                            row = i;
                            column = j;
                        }
                    }
                }

                result += $"{row}{column} ";
            }

            return result.Trim();
        }

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            var encryptedMessageIndices = encryptedMessage.Split(' ');
            _keyword = _keyword.ToUpper().Replace(" ", "");
            _keyword = string.Join(string.Empty, _keyword.ToCharArray().Distinct());

            string result = "";
            string key = _keyword + string.Join("", Characters.Except(_keyword)).Replace("J", "");

            var matrix = CreateMatrix(key);

            foreach (var indices in encryptedMessageIndices)
            {
                int row = indices[0] - '0';
                int column = indices[1] - '0';

                result += matrix[row, column];
            }

            return result;
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
    }
}
