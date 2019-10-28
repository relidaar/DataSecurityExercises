using System;
using DataSecurity.Lab2_2.Encoders;

namespace DataSecurity.Lab2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Rsa();
            Schnorr();
            Ffs();
        }

        static void Rsa()
        {
            Console.WriteLine("RSA");
            var a = new RsaSender(150, 1000);
            var b = new RsaReceiver();

            a.GenerateKeys();
            b.GenerateK(a.n);

            b.CalculateR(a.e, a.n);
            a.CalculateK(b.r);
            Console.WriteLine($"r = {b.r}");

            var result = b.Check(a.k);
            Console.WriteLine($"k' = {a.k}");

            Console.WriteLine($"result: k' = k {result}");
        }

        static void Schnorr()
        {
            Console.WriteLine("Schnorr");
            var a = new SchnorrSender(150, 1000);
            var b = new SchnorrReceiver(10);

            a.GenerateKeys();
            a.GenerateK();
            b.GenerateE();

            a.CalculateR();
            Console.WriteLine($"r = {a.r}");
            Console.WriteLine($"e = {b.e}");

            a.CalculateS(b.e);
            Console.WriteLine($"s = {a.s}");

            var result = b.Check(a.s, a.r, a.p, a.g, a.y);
            Console.WriteLine($"result: r = (g^s * y^e) mod p {result}");
        }

        static void Ffs()
        {
            Console.WriteLine("FFS");
            var provider = new FfsProvider(150, 1000);
            var a = new FfsSender();
            var b = new FfsReceiver();

            provider.GenerateKeys();
            a.GenerateR(provider.n);

            a.CalculateY(provider.n, provider.s);

            a.CalculateZ(provider.n);
            Console.WriteLine($"z = {a.z}");

            bool result;
            var bit = b.GetRandomBit();
            Console.WriteLine($"random bit = {bit}");
            if (bit == 0)
            {
                result = b.Check(a.z, a.r, provider.n);
                Console.WriteLine($"r = {a.r}");

                Console.WriteLine($"result: z = r2 mod n {result}");
            }
            else
            {
                result = b.Check(a.z, a.y, provider.v, provider.n);
                Console.WriteLine($"y = {a.y}");

                Console.WriteLine($"result: z = (y2 * v) mod n {result}");
            }
        }
    }
}
