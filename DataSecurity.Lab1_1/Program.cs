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
            var factory = new EncoderFactory();
            var encoders = new List<IEncoder>
            {
                factory.UseCaesarCipher(),
                factory.UseSloganCipher(),
                factory.UseTrithemiusCipher(),
                factory.UsePolybiusSquare(),
                factory.UseVigenereCipher(),
                factory.UsePlayfairCipher(),
                factory.UseHomophonicCipher()
            };

            while (true)
            {
                Console.Clear();
                Console.Write("Enter the message: ");
                string message = Console.ReadLine()?.ToUpper();
                Console.WriteLine();

                foreach (var encoder in encoders)
                {
                    var result = encoder.Encrypt(message) ?? "Error";
                    Console.WriteLine($"{encoder.Name}: {result}");
                }

                Console.Write("\nContinue? (y/n): ");
                var input = Console.ReadLine();

                if (input == "n") break;
            }
        }
    }
}
