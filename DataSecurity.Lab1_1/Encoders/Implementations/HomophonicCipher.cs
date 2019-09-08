using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class HomophonicCipher : BaseEncoder, IEncoder
    {
        public string Name => "Homophonic cipher";

        private readonly int[] _frequencies;

        private string[][] _key;

        public HomophonicCipher(int[] frequencies) => _frequencies = frequencies;

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper().Replace(" ", "");

            _key = GetKey();

            string result = "";

            var rnd = new Random();
            foreach (var symbol in message)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = Characters.IndexOf(symbol);
                int secondIndex = rnd.Next(0, _frequencies[index]);

                result += _key[index][secondIndex] + " ";
            }

            return result.Trim();
        }

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            var encryptedMessageIndices = encryptedMessage.Split(' ');
            string result = "";

            foreach (var symbol in encryptedMessageIndices)
            {
                for (var i = 0; i < _key.Length; i++)
                {
                    var nums = _key[i];
                    if (!nums.Contains(symbol)) continue;

                    result += Characters[i];
                }
            }

            return result;
        }

        private string[][] GetKey()
        {
            var key = new string[Characters.Length][];

            int n = _frequencies.Sum();
            var nums = new List<string>(n);

            var rnd = new Random();
            while (nums.Count < n)
            {
                int n1 = rnd.Next(0, 10);
                int n2 = rnd.Next(0, 10);
                int n3 = rnd.Next(0, 10);
                string number = $"{n1}{n2}{n3}";

                if (nums.Contains(number)) continue;

                nums.Add(number);
            }
            
            int index = 0;
            int numsIndex = 0;
            foreach (var frequency in _frequencies)
            {
                var symbols = new string[frequency];
                for (int i = 0; i < frequency; i++)
                {
                    symbols[i] = nums[numsIndex];
                    numsIndex++;
                }

                Array.Sort(symbols);
                key[index] = symbols;
                index++;
            }

            return key;
        }
    }
}
