using System;
using System.Collections.Generic;
using DataSecurity.Lab1_2.Encoders;
using DataSecurity.Lab1_2.Encoders.Implementations;

namespace DataSecurity.Lab1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var encoders = new Dictionary<string, IEncoder>
            {
                {"Simple transposition cipher", new SimpleTranspositionEncoder() },
                {"Block transposition cipher", new BlockTranspositionEncoder(3) },
                {"Route cipher", new RouteEncoder(3) },
                {"Vertical transposition cipher", new VerticalTranspositionEncoder("secret") },
                {"Magic square", new MagicSquareEncoder()},
                {"Grille cipher", new GrilleEncoder() },
                {"Double transposition cipher", new DoubleTranspositionEncoder(4) }
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
