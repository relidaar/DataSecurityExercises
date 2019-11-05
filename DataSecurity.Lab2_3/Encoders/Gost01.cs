using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace DataSecurity.Lab2_3.Encoders
{
    class Gost01Sender
    {
        public int r;
        public int s;

        public ECPoint p1;
        public ECPoint p2;

        private BigInteger _h = 19229;
        private int _d;
        private int _k;
        private int _e;
        private ECPoint _c;

        public Gost01Sender(ECPoint p) => p1 = p;

        public void GenerateKeys()
        {
            _d = new Random().Next(1, p1.Q);

            p2 = ECPoint.Multiply(p1, _d);
            
            Console.WriteLine($"P(xp, yp) = ({p1.X},{p1.Y}), Q(xq, yq) = ({p2.X},{p2.Y}), q = {p1.Q}, A = {p1.A}, B = {p2.B}, n = {p1.N}");
        }

        public void CalculateH(string input)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            _h = new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
            Console.WriteLine($"h = {_h}");
        }

        public void CalculatePointC()
        {
            _c = ECPoint.Multiply(p1, _k);

            Console.WriteLine($"C(xc, yc) = ({_c.X},{_c.Y})");
        }

        public void CalculateE()
        {
            _e = (int) (_h % p1.Q);

            Console.WriteLine($"e = {_e}");
        }

        public int CalculateR() => r = _c.X % _c.Q;

        public int CalculateS() => s = (r * _d + _k * _e) % _c.Q;

        public void GenerateK() => _k = new Random().Next(1, p1.Q);
    }

    class Gost01Receiver
    {
        private BigInteger _h = 19229;
        private int _e;
        private int _r;
        private int _v;
        private int _z1, _z2;

        private ECPoint _c;

        public void CalculateH(string input)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            _h = new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
            Console.WriteLine($"h' = {_h}");
        }

        public bool Check(int r) => r == _r;

        public void CalculateR()
        {
            _r = _c.X % _c.Q;

            Console.WriteLine($"r' = {_r}");
        }

        public void CalculateE(int q)
        {
            _e = (int) (_h % q);

            Console.WriteLine($"e' = {_e}");
        }

        public void CalculateV(int q)
        {
            _v = _e.GetInverse(q) % q;

            Console.WriteLine($"v = {_v}");
        }

        public void CalculateZ(int s, int r, int q)
        {
            _z1 = s * _v % q;
            _z2 = (q - r) * _v % q;

            Console.WriteLine($"z1 = {_z1}, z2 = {_z2}");
        }

        public void CalculatePointC(ECPoint p1, ECPoint p2)
        {
            _c = ECPoint.Multiply(p1, _z1) + ECPoint.Multiply(p2, _z2);

            Console.WriteLine($"C'(xc, yc) = ({_c.X},{_c.Y})");
        }
    }
}
