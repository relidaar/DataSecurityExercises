using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab1_3
{
    public class XorEncoder : IEncoder
    {
        private readonly int[] _gamma;

        public XorEncoder(int gammaCount)
        {
            if (gammaCount <= 0) throw new Exception("Gamma cannot be less than or equal to zero");

            _gamma = GenerateGamma(gammaCount).ToArray();
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var gammaIndex = 0;

            var result = new StringBuilder();
            foreach (var symbol in message)
            {
                var gammaBinary = GetBinary(_gamma[gammaIndex]).PadLeft(8, '0');
                var symbolBinary = GetBinary(symbol).PadLeft(8, '0');

                result.Append(EncryptBinary(symbolBinary, gammaBinary) + " ");

                gammaIndex = (gammaIndex + 1) % _gamma.Length;
            }

            return result.ToString().Trim();
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var gammaIndex = 0;

            var result = new StringBuilder();
            foreach (var encryptedBinary in encryptedMessage.Split(' '))
            {
                var gammaBinary = GetBinary(_gamma[gammaIndex]).PadLeft(8, '0');

                result.Append((char) Convert.ToInt32(EncryptBinary(encryptedBinary, gammaBinary), 2));

                gammaIndex = (gammaIndex + 1) % _gamma.Length;
            }

            return result.ToString();
        }

        private static string EncryptBinary(string input, string gamma)
        {
            var result = new StringBuilder();
            for (var i = 0; i < 8; i++)
            {
                if (input[i] == '0' && gamma[i] == '0' ||
                    input[i] == '1' && gamma[i] == '1')
                    result.Append('0');

                if (input[i] == '0' && gamma[i] == '1' ||
                    input[i] == '1' && gamma[i] == '0')
                    result.Append('1');
            }

            return result.ToString();
        }

        private static string GetBinary(int number)
        {
            if (number == 0) return "";

            var remainder = number % 2;
            number /= 2;

            return GetBinary(number) + remainder;
        }

        private static IEnumerable<int> GenerateGamma(int n)
        {
            var used = new List<int>();
            var rnd = new Random();
            for (var i = 0; i < n; i++)
            {
                var number = rnd.Next(33, 126);
                while (used.Contains(number)) number = rnd.Next(33, 126);

                yield return number;
                used.Add(number);
            }
        }
    }
}