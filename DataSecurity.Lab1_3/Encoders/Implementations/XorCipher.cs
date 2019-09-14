using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_3.Encoders.Interfaces;

namespace DataSecurity.Lab1_3.Encoders.Implementations
{
    class XorCipher : BaseEncoder, IEncoder
    {
        public string Name => "XOR cipher";

        private readonly int _gammaCount;
        private int[] _gamma;

        public XorCipher(int gammaCount) => _gammaCount = gammaCount;

        public string Encrypt(string message)
        {
            if (_gammaCount <= 0) return null;
            if (message == null) return null;

            _gamma = GenerateGamma();

            message = message.ToUpper().Replace(" ", "");

            string result = "";
            int gammaIndex = 0;

            foreach (var symbol in message)
            {
                if (!Characters.Contains(symbol)) continue;

                var gammaBinary = ToFullBinary(GetBinary(_gamma[gammaIndex]));
                var symbolBinary = ToFullBinary(GetBinary(symbol));
                var encryptedBinary = "";

                for (int i = 0; i < 8; i++)
                {
                    if (symbolBinary[i] == '0' && gammaBinary[i] == '0' ||
                        symbolBinary[i] == '1' && gammaBinary[i] == '1')
                        encryptedBinary += '0';

                    if (symbolBinary[i] == '0' && gammaBinary[i] == '1' ||
                        symbolBinary[i] == '1' && gammaBinary[i] == '0')
                        encryptedBinary += '1';
                }
                
                result += encryptedBinary + " ";

                gammaIndex = (gammaIndex + 1) % _gamma.Length;
            }

            return result.Trim();
        }

        public string Decrypt(string encryptedMessage)
        {
            if (encryptedMessage == null) return null;

            string result = "";
            int gammaIndex = 0;

            foreach (var encryptedBinary in encryptedMessage.Split(' '))
            {
                var gammaBinary = ToFullBinary(GetBinary(_gamma[gammaIndex]));
                var decryptedBinary = "";

                for (int i = 0; i < 8; i++)
                {
                    if (encryptedBinary[i] == '0' && gammaBinary[i] == '0' ||
                        encryptedBinary[i] == '1' && gammaBinary[i] == '1')
                        decryptedBinary += '0';

                    if (encryptedBinary[i] == '0' && gammaBinary[i] == '1' ||
                        encryptedBinary[i] == '1' && gammaBinary[i] == '0')
                        decryptedBinary += '1';
                }

                result += (char) Convert.ToInt32(decryptedBinary, 2);

                gammaIndex = (gammaIndex + 1) % _gamma.Length;
            }

            return result;
        }

        private string ToFullBinary(string value)
        {
            while (value.Length < 8) value = '0' + value;
            return value;
        }

        private string GetBinary(int number)
        {
            if (number == 0) return "";

            int remainder = number % 2;
            number /= 2;

            return GetBinary(number) + remainder;
        }

        private int[] GenerateGamma()
        {
            var used = new List<int>();
            var gamma = new int[_gammaCount];
            var rnd = new Random();
            for (int i = 0; i < _gammaCount; i++)
            {
                var number = rnd.Next(65, 91);
                while (used.Contains(number)) number = rnd.Next(65, 91);

                gamma[i] = number;
                used.Add(number);
            }

            return gamma;
        }
    }
}
