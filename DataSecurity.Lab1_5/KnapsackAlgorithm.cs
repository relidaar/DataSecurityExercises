using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_5
{
    internal class KnapsackAlgorithm : IEncoder
    {
        private int _m;
        private int _n;
        private int _n1;
        private int[] _privateKey;
        private int[] _publicKey;

        public KnapsackAlgorithm(int keyLength)
        {
            if (keyLength <= 0) return;

            GenerateKey(keyLength);
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var binaryBlocks = message.Select(symbol => ((int) symbol).GetBinary().PadLeft(10, '0')).ToList();

            var result = new List<BigInteger>();
            foreach (var block in binaryBlocks)
                result.Add(block.Select((b, i) => (b - '0') * _publicKey[i]).Sum());

            return string.Join(" ", result).Trim();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var values = encryptedMessage.Split(" ")
                .Select(x => Convert.ToInt32((BigInteger.Parse(x) * _n1 % _m).ToString())).ToList();

            var result = new StringBuilder();
            foreach (var value in values)
            {
                var sum = value;
                var binary = new StringBuilder();
                foreach (var x in _privateKey.Reverse())
                    if (x <= sum)
                    {
                        sum -= x;
                        binary.Append('1');
                    }
                    else
                    {
                        binary.Append('0');
                    }

                var binaryReversed = new string(binary.ToString().Reverse().ToArray());
                result.Append((char) Convert.ToInt32(binaryReversed, 2));
            }

            return result.ToString();
        }

        private void GenerateKey(int length)
        {
            var rnd = new Random();
            _privateKey = GetPrivateKey(length);
            var sum = _privateKey.Sum();

            _m = rnd.Next(sum, sum + length);
            while (MathExtensions.GetNod(_n, _m) != 1) _n = rnd.Next(length, _m);
            while (_n * _n1 % _m != 1) _n1++;

            _publicKey = _privateKey.Select(x => x * _n % _m).ToArray();
        }

        private static int[] GetPrivateKey(int length)
        {
            var rnd = new Random();
            var key = new List<int> {rnd.Next(1, length)};
            while (key.Count < length)
            {
                var sum = key.Sum();
                var number = rnd.Next(sum, sum + length);

                key.Add(number);
            }

            return key.ToArray();
        }
    }
}