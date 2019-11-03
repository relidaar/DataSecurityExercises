using System;
using DataSecurity.Lab2_3.Encoders;

namespace DataSecurity.Lab2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter message: ");
            var input = Console.ReadLine();

            Console.WriteLine("\nRSA");
            var rsaA = new RsaSender(150, 1000);
            var rsaB = new RsaReceiver();

            rsaA.GenerateKeys();
            rsaA.CalculateH(input);
            rsaA.CalculateS();
            Console.WriteLine($"s = {rsaA.s}");

            rsaB.CalculateH(input, rsaA.n);
            var result = rsaB.Check(rsaA.s, rsaA.e, rsaA.n);

            Console.WriteLine($"result: h' = h {result}");
        }
    }
}
