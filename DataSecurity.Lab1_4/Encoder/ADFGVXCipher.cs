using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.String;

namespace DataSecurity.Lab1_4.Encoder
{
    class AdfgvxCipher : IEncoder
    {
        public string Name => "ADFGVX cipher";

        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string Key = "ADFGVX";
        private readonly string _keyword;
        private char[,] _matrix;
        private const char Placeholder = '*';

        public AdfgvxCipher(string keyword) => _keyword = keyword.ToUpper();

        public string Encrypt(string message)
        {
            if (IsNullOrEmpty(message)) return null;

            message = message.ToUpper();

            CreateMatrix();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(_matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            string cipherText = "";
            foreach (var symbol in message)
            {
                if (!Alphabet.Contains(symbol)) continue;

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (_matrix[i, j] == symbol)
                        {
                            cipherText += $"{Key[i]}{Key[j]}";
                        }
                    }
                }
            }
            Console.WriteLine();

            int m = _keyword.Length;
            while (cipherText.Length % m != 0) cipherText += Placeholder;
            int n = cipherText.Length / m;

            var table = new char[n, m];

            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    table[i, j] = cipherText[index];
                    index++;
                }
            }

            var key = CreateKey();

            string result = "";

            foreach (var c in _keyword) Console.Write(c + " ");

            Console.WriteLine();

            foreach (var i in key)
            {
                Console.Write(i + " ");
                for (int j = 0; j < n; j++)
                {
                    var current = table[j, i];
                    result += current;
                }
            }

            Console.WriteLine();

            return result;
        }

        public string Decrypt(string encryptedMessage)
        {
            if (IsNullOrEmpty(encryptedMessage)) return null;

            int m = _keyword.Length;
            while (encryptedMessage.Length % m != 0) encryptedMessage += Placeholder;
            int n = encryptedMessage.Length / m;

            var table = new char[n, m];

            var key = CreateKey();

            int index = 0;
            foreach (var i in key)
            {
                for (int j = 0; j < n; j++)
                {
                    table[j, i] = encryptedMessage[index];
                    index++;
                }
            }

            string cipherText = "";

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    cipherText += table[i, j];
                }
            }

            cipherText = cipherText.Trim(Placeholder);

            string result = "";
            for (var i = 0; i < cipherText.Length; i+=2)
            {
                var first = Key.IndexOf(cipherText[i]);
                var second = Key.IndexOf(cipherText[i+1]);
                result += _matrix[first, second];
            }

            return result;
        }

        private void CreateMatrix()
        {
            var used = new List<int>();
            _matrix = new char[6,6];

            var rnd = new Random();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int index = rnd.Next(0, Alphabet.Length);
                    while (used.Contains(index)) index = rnd.Next(0, Alphabet.Length);

                    _matrix[i, j] = Alphabet[index];
                    used.Add(index);
                }
            }
        }

        private int[] CreateKey()
        {
            var key = new int[_keyword.Length];
            int index = 0;

            foreach (var symbol in Alphabet)
            {
                for (var i = 0; i < _keyword.Length; i++)
                {
                    var keySymbol = _keyword[i];
                    if (symbol != keySymbol) continue;

                    key[i] = index;
                    index++;
                }
            }

            return key;
        }
    }
}
