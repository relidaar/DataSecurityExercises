using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    class RouteCipher : IEncoder
    {
        public string Name => "Route cipher";

        private readonly int columns;

        public RouteCipher(int numberOfColumns) => columns = numberOfColumns;

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper().Replace(" ", "");
            while (message.Length % columns != 0) message += "*";

            int rows = message.Length / columns;

            var table = new char[rows, columns];

            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    table[i, j] = message[index];
                    index++;
                }
            }

            string result = "";

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    result += table[j, i];
                }
            }

            return result;
        }

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            encryptedMessage = encryptedMessage.ToUpper().Replace(" ", "");
            while (encryptedMessage.Length % columns != 0) encryptedMessage += "*";

            int rows = encryptedMessage.Length / columns;

            var table = new char[rows, columns];

            int index = 0;
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    table[j, i] = encryptedMessage[index];
                    index++;
                }
            }

            string result = "";

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result += table[i, j];
                }
            }

            return result;
        }
    }
}
