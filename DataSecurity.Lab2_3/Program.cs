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

            Rsa(input);
            Gost94(input);
            Gost01(input);
        }

        static void Rsa(string input)
        {
            Console.WriteLine("\nRSA");
            var a = new RsaSender(150, 1000);
            var b = new RsaReceiver();

            a.GenerateKeys();
            a.CalculateH(input);
            a.CalculateS();
            Console.WriteLine($"s = {a.s}");

            b.CalculateH(input, a.n);
            var result = b.Check(a.s, a.e, a.n);

            Console.WriteLine($"result: h' = h {result}");
        }

        static void Gost94(string input)
        {
            Console.WriteLine("\nGost94");
            var a = new Gost94Sender(150, 1000);
            var b = new Gost94Receiver();

            Console.WriteLine("A: ");
            a.GenerateKeys();
            a.CalculateH(input);
            while (a.s == 0 || a.w == 0)
            {
                a.CalculateW();
                a.CalculateS();
            }
            Console.WriteLine($"w = {a.w}, s = {a.s}");

            Console.WriteLine("\nB: ");
            b.CalculateH(input);
            b.CalculateV(a.q);
            b.CalculateZ(a.s, a.w, a.q);
            b.CalculateU(a.a, a.y, a.p, a.q);
            var result = b.Check(a.w);

            Console.WriteLine($"result: w' = u {result}");
        }

        static void Gost01(string input)
        {
            Console.WriteLine("\nGost01");
            var a = new Gost01Sender(new ECPoint(7, 17, 3, 7, 41, 47));
            var b = new Gost01Receiver();

            Console.WriteLine("A: ");
            a.GenerateKeys();
            a.CalculateH(input);
            a.CalculateE();
            while (a.s == 0 || a.r == 0)
            {
                a.GenerateK();
                a.CalculatePointC();
                a.CalculateR();
                a.CalculateS();
            }
            Console.WriteLine($"r = {a.r}, s = {a.s}");

            Console.WriteLine("\nB: ");
            b.CalculateH(input);
            b.CalculateE(a.p1.Q);
            b.CalculateV(a.p1.Q);
            b.CalculateZ(a.s, a.r, a.p1.Q);
            b.CalculatePointC(a.p1, a.p2);
            b.CalculateR();
            var result = b.Check(a.r);

            Console.WriteLine($"result: r' = r {result}");

        }
    }
}
