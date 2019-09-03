using System;
using System.Collections.Generic;
using DataSecurity.Lab1_1.Encoders;

namespace DataSecurity.Lab1_1
{
    class Program
    {
        private static List<IEncoder> _encoders = new List<IEncoder>
        {
            new CaesarCipher(-1, "Caesar cipher")
        };

        static void Main(string[] args)
        {
            Console.Write("Enter the message: ");
            string message = Console.ReadLine()?.ToUpper();

            foreach (var encoder in _encoders)
            {
                var encryptedMessage = encoder.Encrypt(message);
                Console.WriteLine($"{encoder.EncoderName}: {encryptedMessage}");
            }
        }
    }
}
