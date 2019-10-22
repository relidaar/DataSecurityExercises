using System;
using DataSecurity.Lab2_1.Encoder;

namespace DataSecurity.Lab2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            IEncoder encoder = new HashMd5();    
            while (true)
            {
                Console.Clear();
                Console.Write("Enter the message: ");
                string message = Console.ReadLine();
                Console.WriteLine();

                var encrypted = encoder.Encode(message) ?? "Error";
                Console.WriteLine(encrypted + "\n");

                Console.Write("\nContinue? (y/n): ");
                var input = Console.ReadLine();

                if (input == "n") break;
            }
        }
    }
}
