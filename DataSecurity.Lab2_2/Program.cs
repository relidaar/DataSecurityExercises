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
            var a = new RsaSender(150, 1000);
            var b = new RsaReceiver();

            a.GenerateKeys();

            b.CalculateR(a.e, a.n);
            a.CalculateK(b.r);

            var result = b.Check(a.k);
            Console.WriteLine($"RSA: k' = k {result}");
        }

        static void Schnorr()
        {
            var a = new SchnorrSender(150, 1000);
            var b = new SchnorrReceiver(10);

            a.GenerateKeys();
            b.GenerateE();

            a.CalculateR();
            a.CalculateS(b.e);

            var result = b.Check(a.s, a.r, a.p, a.g, a.y);
            Console.WriteLine($"Schnorr: r = (g^s * y^e) mod p {result}");
        }

        static void Ffs()
        {
            var provider = new FfsProvider(150, 1000);
            var a = new FfsSender();
            var b = new FfsReceiver();

            provider.GenerateKeys();

            a.CalculateY(provider.n, provider.s);
            a.CalculateZ(provider.n);

            bool result;
            if (b.GetRandomBit() == 0)
            {
                result = b.Check(a.z, a.r, provider.n);
                Console.WriteLine($"FFS: z = r2 mod n {result}");
            }
            else
            {
                result = b.Check(a.z, a.y, provider.v, provider.n);
                Console.WriteLine($"FFS: z = (y2 * v) mod n {result}");
            }
        }
    }
}
