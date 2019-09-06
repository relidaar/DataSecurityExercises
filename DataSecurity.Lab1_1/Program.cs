using System;
using System.Collections.Generic;
using DataSecurity.Lab1_1.Encoders;
using DataSecurity.Lab1_1.Encoders.Implementations;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1
{
    class Program
    {
        private static readonly List<IEncoder> Encoders = new List<IEncoder>
        {
            new CaesarCipher(),
            new SloganCipher(),
            new PolybiusSquare(),
            new TrithemiusCipher(),
            new VigenereCipher(),
            new PlayfairСipher()
        };

        static void Main(string[] args)
        {
            Console.Write("Enter the message: ");
            string message = Console.ReadLine()?.ToUpper();

            foreach (var encoder in Encoders)
            {
                var result = encoder.Encrypt(message) ?? "Error";
                Console.WriteLine($"{encoder.Name}: {result}");
            }
        }
    }
}
