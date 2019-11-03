using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace DataSecurity.Lab2_3.Encoders
{
    class Gost94Sender
    {
        public int p;
        public int q;
        public int a;
        public int y;
        public int w;
        public int s;

        private int _x;
        private BigInteger _h = 19229;
        private int _k;

        private readonly int _lowerBound;
        private readonly int _upperBound;

        public Gost94Sender(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public void CalculateH(string input)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            _h = new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
            Console.WriteLine($"h = {_h}");
        }

        public int CalculateS() => s = (int) ((_x * w + _k * _h) % q);

        public void CalculateW()
        {
            _k = new Random().Next(1, q - 1);
            w = (int)(BigInteger.Pow(a, _k) % p) % q;
        }

        public void GenerateKeys()
        {
            while (true)
            {
                p = Extensions.GetPrime(_upperBound, _lowerBound);
                q = Extensions.GetPrime(_upperBound, _lowerBound);

                if ((p - 1) % q == 0) break;
            }

            _x = new Random().Next(1, q - 1);
            a = GetA();
            y = (int) (BigInteger.Pow(a, _x) % p);

            Console.WriteLine($"p = {p}, q = {q}, a = {a}, y = {y}, x = {_x}");
        }

        private int GetA()
        {
            var rnd = new Random();
            int value = rnd.Next(1, p - 1);
            while (BigInteger.Pow(value, q) % p != 1) value = rnd.Next(1, p - 1);

            return value;
        }
    }

    class Gost94Receiver
    {
        private BigInteger _h = 19229;
        private int _z1, _z2;
        private int _u;
        private int _v;

        public bool Check(int w) => w == _u;

        public void CalculateV(int q)
        {
            _v = (int) (BigInteger.Pow(_h, q - 2) % q);
            Console.WriteLine($"v = {_v}");
        }

        public void CalculateU(int a, int y, int p, int q)
        {
            _u = (int) (BigInteger.Pow(a, _z1) * BigInteger.Pow(y, _z2) % p % q);
            Console.WriteLine($"u = {_u}");
        }

        public void CalculateZ(int s, int w, int q)
        {
            _z1 = s * _v % q;
            _z2 = (q - w) * _v % q;

            Console.WriteLine($"z1 = {_z1}, z2 = {_z2}");
        }

        public void CalculateH(string input)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            _h = new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
        }

    }
}
