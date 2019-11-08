using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_2
{
    internal class BlockTranspositionEncoder : IEncoder
    {
        private readonly int _blockSize;
        private List<int[]> _key;

        private readonly char _placeholder = '*';

        public BlockTranspositionEncoder(int blockSize)
        {
            if (blockSize <= 0) throw new Exception("Block size cannot be equal to or less than zero");

            _blockSize = blockSize;
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            message = message.Replace(" ", "");
            while (message.Length % _blockSize != 0) message += _placeholder;

            _key = CreateKey(message.Length, _blockSize).ToList();

            var input = CreateMessageBlocks(message, _blockSize);

            var result = new StringBuilder();
            foreach (var block in input.Zip(_key, (i, k) => new {Input = i, Key = k}))
            foreach (var index in block.Key)
                result.Append(block.Input[index]);

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            encryptedMessage = encryptedMessage.Replace(" ", "");

            var input = CreateMessageBlocks(encryptedMessage, _blockSize);

            var result = new StringBuilder();
            foreach (var block in input.Zip(_key, (i, k) => new {Input = i, Key = k}))
                for (var i = 0; i < _blockSize; i++)
                {
                    var index = Array.IndexOf(block.Key, i);
                    result.Append(block.Input[index]);
                }

            return result.ToString().Trim(_placeholder);
        }

        private static IEnumerable<int[]> CreateKey(int messageLength, int blockSize)
        {
            for (var i = 0; i < messageLength / blockSize; i++)
            {
                var block = new int[blockSize];
                var used = new List<int>();

                var rnd = new Random();

                // Generate random positions for each block
                for (var j = 0; j < blockSize; j++)
                {
                    var pos = rnd.Next(0, blockSize);
                    while (used.Contains(pos)) pos = rnd.Next(0, blockSize);

                    block[j] = pos;
                    used.Add(pos);
                }

                yield return block;
            }
        }

        private static IEnumerable<string> CreateMessageBlocks(string message, int size)
        {
            for (var i = 0; i < message.Length; i += size)
            {
                var str = new StringBuilder();
                for (var j = i; j < i + size; j++) str.Append(message[j]);

                yield return str.ToString();
            }
        }
    }
}