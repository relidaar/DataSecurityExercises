using System;
using DataSecurity.Lab2_4;

namespace DataSecurity.LabTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var encoder = new ParityBits();

            var result = encoder.Encode(input);

            Console.WriteLine($"Parity bits: {result}");
        }
    }
}
