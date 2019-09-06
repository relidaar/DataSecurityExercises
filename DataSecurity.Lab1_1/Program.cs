using System;
using System.Collections.Generic;
using DataSecurity.Lab1_1.Encoders;
using DataSecurity.Lab1_1.Encoders.Implementations;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the message: ");
            string message = Console.ReadLine()?.ToUpper();

            var factory = new EncoderFactory();
            var encoders = new List<IEncoder>
            {
                factory.UseCaesarCipher(),
                factory.UseSloganCipher(),
                factory.UseTrithemiusCipher(),
                factory.UsePolybiusSquare(),
                factory.UseVigenereCipher(),
                factory.UsePlayfairCipher()
            };

            foreach (var encoder in encoders)
            {
                var result = encoder.Encrypt(message) ?? "Error";
                Console.WriteLine($"{encoder.Name}: {result}");
            }
        }
    }
}
