using System;
using System.Collections.Generic;

namespace DataSecurity.Lab1_1
{
    class Program
    {
        private static List<IEncoder> _encoders = new List<IEncoder>
        {
        };

        static void Main(string[] args)
        {
            string message = Console.ReadLine();

            foreach (var encoder in _encoders)
            {
                var encryptedMessage = encoder.Encrypt(message);
                Console.WriteLine($"{encoder.EncoderName}: {encryptedMessage}");
            }
        }
    }
}
