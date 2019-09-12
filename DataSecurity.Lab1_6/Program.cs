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
                factory.UseDirectBinary()
            };

            while (true)
            {
                Console.Clear();
                Console.Write("Enter the number: ");
                string number = Console.ReadLine();
                Console.WriteLine();

                foreach (var encoder in encoders)
                {
                    var encrypted = encoder.Encrypt(number) ?? "Error";
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
