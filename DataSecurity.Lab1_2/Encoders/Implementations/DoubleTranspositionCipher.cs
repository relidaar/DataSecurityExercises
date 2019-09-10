using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    class DoubleTranspositionCipher : IEncoder
    {
        public string Name => "Double transposition cipher";

        private readonly int _blockSize;

        private int[] _rows;
        private int[] _columns;
        private char _placeholder = '*';

        public DoubleTranspositionCipher(int blockSize) => _blockSize = blockSize;

        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            message = message.ToUpper().Replace(" ", "");
            while (message.Length % (_blockSize * _blockSize) != 0) message += _placeholder;

            _rows = GenerateNums();
            _columns = GenerateNums();

            var messageBlocks = CreateMessageBlocks(message);
            var encryptedBlocks = new List<char[,]>();

            foreach (var messageBlock in messageBlocks)
            {
                var encryptedBlock = CreateEncryptedBlock(messageBlock);
                encryptedBlocks.Add(encryptedBlock);
            }

            string result = "";

            foreach (var encryptedBlock in encryptedBlocks)
            {
                for (int i = 0; i < _blockSize; i++)
                {
                    for (int j = 0; j < _blockSize; j++)
                    {
                        result += encryptedBlock[j, i];
                    }
                }
            }

            return result;
        }

        public string Decrypt(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return null;

            var encryptedBlocks = CreateEncryptedMessageBlocks(encryptedMessage);

            string result = "";

            foreach (var encryptedBlock in encryptedBlocks)
            {
                for (int i = 0; i < _blockSize; i++)
                {
                    for (int j = 0; j < _blockSize; j++)
                    {
                        result += encryptedBlock[i, j];
                    }
                }
            }

            return result.Trim(_placeholder);
            //var decryptedBlocks = new List<char[,]>();

            //foreach (var encryptedBlock in encryptedBlocks)
            //{
            //    var decryptedBlock = CreateDecryptedBlock(encryptedBlock);
            //    decryptedBlocks.Add(decryptedBlock);
            //}

            //string result = "";

            //foreach (var decryptedBlock in decryptedBlocks)
            //{
            //    for (int i = 0; i < _blockSize; i++)
            //    {
            //        for (int j = 0; j < _blockSize; j++)
            //        {
            //            result += decryptedBlock[i, j];
            //        }
            //    }
            //}

            //return result;
        }

        private char[,] CreateEncryptedBlock(char[,] messageBlock)
        {
            int n = _blockSize;
            var block = new char[n, n];
            int index = 0;
            while (index != n)
            {
                for (int i = 0; i < n; i++)
                {
                    if (_columns[i] != index || index == n) continue;

                    var column = GetColumn(messageBlock, i);
                    for (int j = 0; j < n; j++)
                    {
                        block[j, i] = column[j];
                    }
                    index++;
                }
            }

            index = 0;
            while (index != n)
            {
                for (int i = 0; i < n; i++)
                {
                    if (_rows[i] != index || index == n) continue;

                    var row = GetRow(messageBlock, i);
                    for (int j = 0; j < n; j++)
                    {
                        block[i, j] = row[j];
                    }
                    index++;
                }
            }

            return block;
        }

        private char[,] CreateDecryptedBlock(char[,] messageBlock)
        {
            int n = _blockSize;
            var block = new char[n, n];
            int index = 0;
            while (index != n)
            {
                for (int i = 0; i < n; i++)
                {
                    if (_columns[i] != index || index == n) continue;

                    var column = GetColumn(messageBlock, i);
                    for (int j = 0; j < n; j++)
                    {
                        block[j, Array.IndexOf(_columns, i)] = column[j];
                    }
                    index++;
                }
            }

            index = 0;
            while (index != n)
            {
                for (int i = 0; i < n; i++)
                {
                    if (_rows[i] != index || index == n) continue;

                    var row = GetRow(messageBlock, i);
                    for (int j = 0; j < n; j++)
                    {
                        block[Array.IndexOf(_rows, i), j] = row[j];
                    }
                    index++;
                }
            }

            return block;
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

        private int[] GenerateNums()
        {
            int n = _blockSize;
            var nums = new int[n];
            var used = new List<int>();

            var rnd = new Random();
            for (int i = 0; i < _blockSize; i++)
            {
                int num = rnd.Next(0, n);
                while (used.Contains(num)) num = rnd.Next(0, n);

                nums[i] = num;
                used.Add(num);
            }

            return nums;
        }

        private List<char[,]> CreateMessageBlocks(string message)
        {
            int n = _blockSize;
            var blocks = new List<char[,]>();

            int index = 0;
            while (index < message.Length)
            {
                var block = new char[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        block[i, j] = message[index];
                        index++;
                    }
                }
                blocks.Add(block);
            }

            return blocks;
        }

        private List<char[,]> CreateEncryptedMessageBlocks(string message)
        {
            int n = _blockSize;
            var blocks = new List<char[,]>();

            int index = 0;
            while (index < message.Length)
            {
                var block = new char[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        block[j, i] = message[index];
                        index++;
                    }
                }
                blocks.Add(block);
            }

            return blocks;
        }
    }
}
