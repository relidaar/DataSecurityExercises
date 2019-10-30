using System;
using System.Collections.Generic;
using DataSecurity.Lab1_1.Encoders.Implementations;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var encoders = new Dictionary<string, IEncoder>
            {
                {"Caesar cipher", new CaesarEncoder(3)},
                {"Slogan cipher", new SloganEncoder("secret")},
                {"Polybius cipher", new PolybiusEncoder("secret")},
                {"Trithemius cipher", new TrithemiusEncoder(2, 5, 3)},
                {"Vigenere cipher", new VigenereEncoder("secret")},
                {"Playfair cipher", new PlayfairEncoder("secret")},
                {
                    "Homophonic cipher",
                    new HomophonicEncoder(new[]
                    {
                        8, 2, 3, 4, 12, 2, 2, 6, 6, 1, 1, 4, 2, 6, 7, 2, 1, 6, 6, 9, 3, 1, 2, 1, 2, 1
                    })
                }
            };

            while (true)
            {
                Console.Clear();
                Console.Write("Enter the message: ");
                var message = Console.ReadLine()?.ToUpper();
                Console.WriteLine();

                foreach (var (encoderName, encoder) in encoders)
                {
                    var encrypted = encoder.Encode(message);
                    var decrypted = encoder.Decode(encrypted);
                    Console.WriteLine($"{encoderName}: {encrypted} ({decrypted})\n");
                }

                Console.Write("\nContinue? (y/n): ");
                var input = Console.ReadLine();

                if (input == "n") break;
            }
        }
    }
}