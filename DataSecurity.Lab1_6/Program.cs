using System;
using System.Collections.Generic;
using DataSecurity.Lab1_6.Encoders;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6
{

    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new[] {111, -112, -113, 114, -114};
            var factory = new EncoderFactory();
            var encoders = new List<IEncoder>
            {
                factory.UseDirectBinary(),
                factory.UseReverseBinary(),
                factory.UseAdditionalBinary(),
                factory.UseSingleBinary()
            };

            while (true)
            {
                Console.Clear();
                Console.Write("Enter the number or numbers: ");
                var input = Console.ReadLine()?.Split(" ");
                Console.WriteLine();

                foreach (var encoder in encoders)
                {
                    Console.Write($"{encoder.Name}: ");
                    foreach (var number in input)
                    {
                        var encrypted = encoder.Encrypt(number) ?? "Error";
                        Console.Write($"{encrypted}({number}); ");
                    }
                    Console.WriteLine("\n");
                }

                Console.Write("\nContinue? (y/n): ");
                string answer = Console.ReadLine();

                if (answer == "n") break;
            }
        }
    }
}
