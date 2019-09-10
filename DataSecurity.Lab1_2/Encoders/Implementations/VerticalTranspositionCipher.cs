using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2.Encoders.Implementations
{
    class VerticalTranspositionCipher : BaseEncoder, IEncoder
    {
        public string Name => "Vertical transposition cipher";

        private readonly string _keyword;
        private char _placeholder = '*';

        public VerticalTranspositionCipher(string keyword) => _keyword = keyword.ToUpper().Replace(" ", "");

        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            var key = CreateKey();

            int m = _keyword.Length;

            message = message.ToUpper().Replace(" ", "");
            while (message.Length % m != 0) message += _placeholder;

            int n = message.Length / m;

            var table = new char[n, m];

            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    table[i, j] = message[index];
                    index++;
                }
            }

            string result = "";

            foreach (var i in key)
            {
                for (int j = 0; j < n; j++)
                {
                    result += table[j, i];
                }
            }

            return result;
        }

        public string Decrypt(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return null;

            var key = CreateKey();

            int m = _keyword.Length;

            encryptedMessage = encryptedMessage.ToUpper().Replace(" ", "");
            while (encryptedMessage.Length % m != 0) encryptedMessage += _placeholder;

            int n = encryptedMessage.Length / m;

            var table = new char[n, m];

            int index = 0;
            foreach (var i in key)
            {
                for (int j = 0; j < n; j++)
                {
                    table[j, i] = encryptedMessage[index];
                    index++;
                }
            }

            string result = "";

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result += table[i, j];
                }
            }

            return result.Trim(_placeholder);
        }

        private int[] CreateKey()
        {
            var key = new int[_keyword.Length];
            int index = 0;

            foreach (var symbol in Characters)
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
