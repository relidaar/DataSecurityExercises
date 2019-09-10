using System;
using System.Collections.Generic;
using DataSecurity.Lab1_2.Encoders;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new EncoderFactory();
            var encoders = new List<IEncoder>
            {
                factory.UseSimpleTranspositionCipher(),
                factory.UseBlockTranspositionCipher(),
                factory.UseRouteCipher(),
                factory.UseVerticalTranspositionCipher(),
                factory.UseMagicSquare(),
                factory.UseDoubleTranspositionCipher()
            };

            while (true)
            {
                Console.Clear();
                Console.Write("Enter the message: ");
                string message = Console.ReadLine()?.ToUpper();
                Console.WriteLine();

                foreach (var encoder in encoders)
                {
                    var encrypted = encoder.Encrypt(message) ?? "Error";
                    var decrypted = encoder.Decrypt(encrypted) ?? "Error";
                    Console.WriteLine($"{encoder.Name}: {encrypted} ({decrypted})\n");
                }

                Console.Write("\nContinue? (y/n): ");
                var input = Console.ReadLine();

                if (input == "n") break;
            }
        }
    }
}
