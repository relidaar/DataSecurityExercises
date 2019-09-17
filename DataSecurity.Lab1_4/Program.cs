using System;
using DataSecurity.Lab1_4.Encoder;

namespace DataSecurity.Lab1_4
{
    class Program
    {
        static void Main(string[] args)
        {
            IEncoder encoder = new AdfgvxCipher("secret");
            while (true)
            {
                Console.Clear();
                Console.Write("Enter the message: ");
                string message = Console.ReadLine()?.ToUpper();
                Console.WriteLine();

                var encrypted = encoder.Encrypt(message) ?? "Error";
                var decrypted = encoder.Decrypt(encrypted) ?? "Error";
                Console.WriteLine($"{encoder.Name}: {encrypted} ({decrypted})\n");

                Console.Write("\nContinue? (y/n): ");
                var input = Console.ReadLine();

                if (input == "n") break;
            }
        }
    }
}
