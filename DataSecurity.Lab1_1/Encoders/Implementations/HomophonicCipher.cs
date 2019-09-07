using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders.Implementations
{
    class HomophonicCipher : BaseEncoder, IEncoder
    {
        public string Name => "Homophonic cipher";

        private readonly int[] _frequencies;

        public HomophonicCipher(int[] frequencies) => _frequencies = frequencies;

        public string Encrypt(string message)
        {
            if (message == null) return null;

            message = message.ToUpper().Replace(" ", "");

            var key = GetKey();

            string result = "";

            var rnd = new Random();
            foreach (var symbol in message)
            {
                if (!Characters.Contains(symbol)) continue;

                int index = Characters.IndexOf(symbol);
                int secondIndex = rnd.Next(0, _frequencies[index]);

                int number = key[index][secondIndex];
                result += number + " ";
            }

            return result;
        }

        private int[][] GetKey()
        {
            var key = new int[Characters.Length][];

            int index = 0;
            foreach (var frequency in _frequencies)
            {
                var symbols = new int[frequency];
                var rnd = new Random();
                for (int i = 0; i < frequency; i++)
                {
                    int n1 = rnd.Next(0, 10);
                    int n2 = rnd.Next(0, 10);
                    int n3 = rnd.Next(0, 10);
                    symbols[i] = int.Parse($"{n1}{n2}{n3}");
                }

                Array.Sort(symbols);
                key[index] = symbols;
                index++;
            }

            return key;
        }
    }
}
