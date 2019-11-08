using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;
using static System.String;

namespace DataSecurity.Lab1_4
{
    public class AdfgvxCipher : IEncoder
    {
        private const string Key = "ADFGVX";
        private const char Placeholder = '*';
        private readonly string _characters;
        private readonly string _keyword;
        private char[,] _matrix;

        public AdfgvxCipher(string keyword)
        {
            if (keyword == null) throw new NullReferenceException();

            _characters = Join("", EncoderExtensions.GenerateAlphabet(26, 65).ToArray());
            _characters += Join("", EncoderExtensions.GenerateAlphabet(10, 48).ToArray());

            _keyword = keyword.ToUpper();

            _matrix = CreateMatrix();
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 6; j++) Console.Write(_matrix[i, j] + " ");
                Console.WriteLine();
            }

            var cipherText = new StringBuilder();
            foreach (var symbol in message.Where(symbol => _characters.Contains(symbol)))
                for (var i = 0; i < 6; i++)
                for (var j = 0; j < 6; j++)
                    if (_matrix[i, j] == symbol)
                        cipherText.Append($"{Key[i]}{Key[j]}");
            Console.WriteLine();

            var m = _keyword.Length;
            while (cipherText.Length % m != 0) cipherText.Append(Placeholder);
            var n = cipherText.Length / m;

            var table = new char[n, m];

            var index = 0;
            for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
            {
                table[i, j] = cipherText[index];
                index++;
            }

            var key = CreateKey();

            var result = new StringBuilder();

            foreach (var c in _keyword) Console.Write(c + " ");

            Console.WriteLine();

            foreach (var i in key)
            {
                Console.Write(i + " ");
                for (var j = 0; j < n; j++) result.Append(table[j, i]);
            }

            Console.WriteLine();

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var m = _keyword.Length;
            while (encryptedMessage.Length % m != 0) encryptedMessage += Placeholder;
            var n = encryptedMessage.Length / m;

            var table = new char[n, m];

            var key = CreateKey();

            var index = 0;
            foreach (var i in key)
                for (var j = 0; j < n; j++)
                {
                    table[j, i] = encryptedMessage[index];
                    index++;
                }

            var cipherText = new StringBuilder();
            for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
                cipherText.Append(table[i, j]);

            cipherText = new StringBuilder(cipherText.ToString().Trim(Placeholder));

            var result = new StringBuilder();
            for (var i = 0; i < cipherText.Length; i += 2)
            {
                var first = Key.IndexOf(cipherText[i]);
                var second = Key.IndexOf(cipherText[i + 1]);
                result.Append(_matrix[first, second]);
            }

            return result.ToString();
        }

        private char[,] CreateMatrix()
        {
            var used = new List<int>();
            _matrix = new char[6, 6];

            var rnd = new Random();
            for (var i = 0; i < 6; i++)
            for (var j = 0; j < 6; j++)
            {
                var index = rnd.Next(0, _characters.Length);
                while (used.Contains(index)) index = rnd.Next(0, _characters.Length);

                _matrix[i, j] = _characters[index];
                used.Add(index);
            }

            return _matrix;
        }

        private IEnumerable<int> CreateKey()
        {
            var index = 0;
            foreach (var symbol in _characters)
            foreach (var keySymbol in _keyword.Where(keySymbol => symbol == keySymbol))
            {
                yield return index;
                index++;
            }
        }
    }
}