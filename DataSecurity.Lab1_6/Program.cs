using System;
using System.Collections.Generic;
using DataSecurity.Lab1_6.Encoders;
using DataSecurity.Lab1_6.Encoders.Implementations;

namespace DataSecurity.Lab1_6
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var numbers = new[] {111, -112, -113, 114, -114};
            var encoders = new Dictionary<string, IEncoder>
            {
                {"Direct binary", new DirectBinaryEncoder()},
                {"Additional binary", new AdditionalBinaryEncoder()},
                {"Reverse binary", new ReverseBinaryEncoder()},
                {"Single binary", new SingleBinaryEncoder()},
                {"Float binary", new FloatBinaryEncoder()}
            };

            while (true)
            {
                Console.Clear();
                Console.Write("Enter the number or numbers: ");
                var input = Console.ReadLine()?.Split(" ");
                Console.WriteLine();

                foreach (var (encoderName, encoder) in encoders)
                {
                    Console.Write($"{encoderName}: ");
                    foreach (var value in input)
                    {
                        var encrypted = encoder.Encode(value);
                        Console.Write($"{encrypted}({value}); ");
                    }

                    Console.WriteLine("\n");
                }

                Console.Write("\nContinue? (y/n): ");
                var answer = Console.ReadLine();

                if (answer == "n") break;
            }
        }
    }
}