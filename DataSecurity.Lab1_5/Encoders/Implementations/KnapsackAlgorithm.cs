using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using DataSecurity.Lab1_5.Encoders.Interfaces;

namespace DataSecurity.Lab1_5.Encoders.Implementations
{
    class KnapsackAlgorithm : IEncoder
    {
        public string Name => "Knapsack algorithm";

        private readonly int _keyLength;

        private int[] _privateKey;
        private int[] _publicKey;
        private int _m;
        private int _n;
        private int _n1;

        public KnapsackAlgorithm(int keyLength)
        {
            if (keyLength <= 0) return;

            _keyLength = keyLength;
        }

        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            GenerateKey();
            var binaryBlocks = message.Select(symbol => ((int) symbol).GetBinary().ToFullBinary(10)).ToList();

            string result = "";
            foreach (var block in binaryBlocks)
                result += block.Select((b, i) => (b - '0') * _publicKey[i]).Sum() + " ";

            return result.Trim();
        }

        public string Decrypt(string encryptedMessage)
        {
            if (string.IsNullOrEmpty(encryptedMessage)) return null;

            var values = encryptedMessage.Split(" ")
                .Select(x => Convert.ToInt32((BigInteger.Parse(x) * _n1 % _m).ToString())).ToList();

            var result = "";
            foreach (var value in values)
            {
                var sum = value;
                var binary = "";
                foreach (var x in _privateKey.Reverse())
                {
                    if (x <= sum)
                    {
                        sum -= x;
                        binary += '1';
                    }
                    else
                    {
                        binary += '0';
                    }
                }

                binary = new string(binary.Reverse().ToArray());
                result += (char)Convert.ToInt32(binary, 2);
            }

            return result;
        }

        private void GenerateKey()
        {
            var rnd = new Random();
            _privateKey = GetPrivateKey();
            var sum = _privateKey.Sum();

            _m = rnd.Next(sum, sum + _keyLength);
            while (Extensions.GetNod(_n, _m) != 1) _n = rnd.Next(_keyLength, _m);
            while (_n * _n1 % _m != 1) _n1++;

            _publicKey = _privateKey.Select(x => x * _n % _m).ToArray();
        }

        private int[] GetPrivateKey()
        {
            var rnd = new Random();
            var key = new List<int> {rnd.Next(1, _keyLength)};
            while (key.Count < _keyLength)
            {
                var sum = key.Sum();
                var number = rnd.Next(sum, sum + _keyLength);

                key.Add(number);
            }

            return key.ToArray();
        }
    }
}
