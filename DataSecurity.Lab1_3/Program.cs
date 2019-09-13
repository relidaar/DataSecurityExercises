using System;
using System.Collections.Generic;
using DataSecurity.Lab1_3.Encoders;
using DataSecurity.Lab1_3.Encoders.Interfaces;

namespace DataSecurity.Lab1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new EncoderFactory();
            var encoders = new List<IEncoder>
            {
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
