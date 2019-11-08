using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_1
{
    public class HomophonicEncoder : IEncoder
    {
        private readonly string _characters;
        private readonly int[] _frequencies;
        private readonly IList<int[]> _key;

        public HomophonicEncoder(int[] frequencies)
        {
            _frequencies = frequencies ?? throw new NullReferenceException();
            _key = GetKey().ToList();
            _characters = string.Join("", EncoderExtensions.GenerateAlphabet(94, 32).ToArray());
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder();
            var rnd = new Random();
            foreach (var symbol in message)
            {
                var index = _characters.IndexOf(symbol);
                var secondIndex = rnd.Next(0, _frequencies[index]);

                result.Append(_key[index][secondIndex] + " ");
            }

            return result.ToString().Trim();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var result = new StringBuilder();
            foreach (var value in encryptedMessage.Split(' ').Select(x => Convert.ToInt32(x)))
            {
                var el = _key.First(x => x.Contains(value));
                var index = _key.IndexOf(el);
                result.Append(_characters[index]);
            }

            return result.ToString();
        }

        private IEnumerable<int[]> GetKey()
        {
            var rnd = new Random();
            var used = new List<int>();
            foreach (var frequency in _frequencies)
            {
                var symbols = new int[frequency];
                for (var i = 0; i < frequency; i++)
                {
                    var number = rnd.Next(100, 1000);
                    while (used.Contains(number)) number = rnd.Next(100, 1000);

                    symbols[i] = number;
                    used.Add(number);
                }

                Array.Sort(symbols);
                yield return symbols;
            }
        }
    }
}