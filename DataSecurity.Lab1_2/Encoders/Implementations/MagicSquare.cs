using System;
using System.Collections.Generic;
using System.Linq;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    public class MagicSquare : IEncoder
    {
        private readonly int[,] _key = { { 16, 3, 2, 13 }, { 9, 6, 7, 12 }, { 5, 10, 11, 8 }, { 4, 15, 14, 1 } };

        private static char _placeholder = '*';

        public string Name => "Magic square";
        
        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            message = message.ToUpper().Replace(" ", "");
            while (message.Length % 16 != 0) message += _placeholder;

            var messageBlocks = CreateMessageBlocks(message, 16);

            string result = "";
            foreach (var messageBlock in messageBlocks)
            {
                var block = CreateEncryptedBlock(messageBlock);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result += block[i, j];
                    }
                }
            }

            return result;
        }

        public string Decrypt(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return null;

            var messageBlocks = CreateMessageBlocks(encryptedMessage, 16);

            string result = "";
            foreach (var messageBlock in messageBlocks)
            {
                var block = CreateEncryptedBlock(messageBlock);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result += block[i, j];
                    }
                }
            }

            return result.Trim(_placeholder);
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
                        if (_key[i, j] == index + 1 && index != messageBlock.Length)
                        {
                            block[i, j] = messageBlock[index];
                            index++;
                        }
                    }
                }
            }

            return block;
        }

        private string[] CreateMessageBlocks(string message, int n)
        {
            var blocks = new List<string>();

            for (var i = 0; i < message.Length; i += n)
            {
                string str = "";

                for (int j = i; j < i + n; j++)
                {
                    str += message[j];
                }

                blocks.Add(str);
            }

            return blocks.ToArray();
        }
    }
}