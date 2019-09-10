using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    class BlockTranspositionCipher : IEncoder
    {
        private List<int[,]> _key;
        private readonly int _blockSize;

        private char _placeholder = '*';

        public BlockTranspositionCipher(int blockSize) => _blockSize = blockSize;

        public string Name => "Block transposition cipher";

        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            message = message.ToUpper().Replace(" ", "");
            while (message.Length % _blockSize != 0) message += _placeholder;

            int n = message.Length / _blockSize;

            CreateKey(n);

            var input = CreateMessageBlocks(message);

            string result = "";
            for (var i = 0; i < n; i++)
            {
                var block = _key[i];
                var blockInput = input[i];
                var blockResult = new char[_blockSize];

                for (int j = 0; j < _blockSize; j++)
                {
                    blockResult[block[1, j]] = blockInput[block[0, j]];
                }

                result += string.Concat(blockResult);
            }

            return result;
        }

        public string Decrypt(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return null;

            encryptedMessage = encryptedMessage.ToUpper().Replace(" ", "");

            int n = encryptedMessage.Length / _blockSize;

            var input = CreateMessageBlocks(encryptedMessage);

            string result = "";
            for (var i = 0; i < n; i++)
            {
                var block = _key[i];
                var blockInput = input[i];
                var blockResult = new char[_blockSize];

                for (int j = 0; j < _blockSize; j++)
                {
                    blockResult[block[0, j]] = blockInput[block[1, j]];
                }

                result += string.Concat(blockResult);
            }

            return result.Trim(_placeholder);
        }

        private void CreateKey(int n)
        {
            _key = new List<int[,]>();
            for (int i = 0; i < n; i++)
            {
                var block = new int[2, _blockSize];
                var used = new List<int>();

                var rnd = new Random();
                for (int j = 0; j < _blockSize; j++)
                {
                    int pos = rnd.Next(0, _blockSize);
                    while (used.Contains(pos)) pos = rnd.Next(0, _blockSize);

                    block[0, j] = j;
                    block[1, j] = pos;
                    used.Add(pos);
                }

                _key.Add(block);
            }
        }

        private string[] CreateMessageBlocks(string message)
        {
            int n = _blockSize;
            var blocks = new List<string>();

            for (var i = 0; i < message.Length; i+=n)
            {
                string str = "";

                for (int j = i; j < i+n; j++)
                {
                    str += message[j];
                }

                blocks.Add(str);
            }

            return blocks.ToArray();
        }
    }
}
