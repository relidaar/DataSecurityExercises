using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    public class MagicSquareEncoder : IEncoder
    {
        private readonly int[,] _key = { { 16, 3, 2, 13 }, { 9, 6, 7, 12 }, { 5, 10, 11, 8 }, { 4, 15, 14, 1 } };

        private const char Placeholder = '*';

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            message = message.Replace(" ", "");
            while (message.Length % 16 != 0) message += Placeholder;

            var messageBlocks = CreateMessageBlocks(message, 16);

            var result = new StringBuilder();
            foreach (var messageBlock in messageBlocks)
            {
                var block = CreateEncryptedBlock(messageBlock);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result.Append(block[i, j]);
                    }
                }
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var messageBlocks = CreateMessageBlocks(encryptedMessage, 16);

            var result = new StringBuilder();
            foreach (var messageBlock in messageBlocks)
            {
                var block = CreateEncryptedBlock(messageBlock);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result.Append(block[i, j]);
                    }
                }
            }

            return result.ToString().Trim(Placeholder);
        }

        private char[,] CreateEncryptedBlock(string messageBlock)
        {
            var block = new char[4, 4];
            int index = 0;
            while (index != messageBlock.Length)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_key[i, j] != index + 1 || index == messageBlock.Length) continue;

                        block[i, j] = messageBlock[index];
                        index++;
                    }
                }
            }

            return block;
        }

        private static IEnumerable<string> CreateMessageBlocks(string message, int n)
        {
            for (var i = 0; i < message.Length; i += n)
            {
                var str = new StringBuilder();
                for (int j = i; j < i + n; j++) str.Append(message[j]);

                yield return str.ToString();
            }
        }
    }
}