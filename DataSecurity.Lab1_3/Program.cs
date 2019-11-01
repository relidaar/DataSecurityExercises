using System;
using System.Collections.Generic;
using DataSecurity.Lab1_3.Encoders;
using DataSecurity.Lab1_3.Encoders.Implementations;
using DataSecurity.Lab1_3.Encoders.Interfaces;

namespace DataSecurity.Lab1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var encoders = new Dictionary<string, IEncoder>
            {
                {"Gamma cipher", new GammaEncoder(10) },
                {"XOR cipher", new XorEncoder(10) }
            };

            while (true)
            {
                Console.Clear();
                Console.Write("Enter the message: ");
                string message = Console.ReadLine();
                Console.WriteLine();

                foreach (var (encoderName, encoder) in encoders)
                {
                    var encrypted = encoder.Encode(message) ?? "Error";
                    var decrypted = encoder.Decode(encrypted) ?? "Error";
                    Console.WriteLine($"{encoderName}: {encrypted} ({decrypted})\n");
                }

                Console.Write("\nContinue? (y/n): ");
                var input = Console.ReadLine();

                if (input == "n") break;
            }
        }
    }
}
