using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_3
{
    public class Gost94Sender
    {
        private readonly int _lowerBound;
        private readonly int _upperBound;
        private BigInteger _h = 19229;
        private int _k;

        private int _x;
        public int A;
        public int P;
        public int Q;
        public int S;
        public int W;
        public int Y;

        public Gost94Sender(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public void CalculateH(string input)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                _h = new BigInteger(hash.Concat(new byte[] {0}).ToArray());
            }
        }

        public int CalculateS()
        {
            return S = (int) ((_x * W + _k * _h) % Q);
        }

        public void CalculateW()
        {
            _k = new Random().Next(1, Q - 1);
            W = (int) (BigInteger.Pow(A, _k) % P) % Q;
        }

        public void GenerateKeys()
        {
            while (true)
            {
                P = MathExtensions.GetPrime(_upperBound, _lowerBound);
                Q = MathExtensions.GetPrime(_upperBound, _lowerBound);

                if ((P - 1) % Q == 0) break;
            }

            _x = new Random().Next(1, Q - 1);
            A = GetA();
            Y = (int) (BigInteger.Pow(A, _x) % P);
        }

        private int GetA()
        {
            var rnd = new Random();
            var value = rnd.Next(1, P - 1);
            while (BigInteger.Pow(value, Q) % P != 1) value = rnd.Next(1, P - 1);

            return value;
        }
    }

    public class Gost94Receiver
    {
        private BigInteger _h = 19229;
        private int _u;
        private int _v;
        private int _z1, _z2;

        public bool Check(int w)
        {
            return w == _u;
        }

        public void CalculateV(int q)
        {
            _v = (int) (BigInteger.Pow(_h, q - 2) % q);
        }

        public void CalculateU(int a, int y, int p, int q)
        {
            _u = (int) (BigInteger.Pow(a, _z1) * BigInteger.Pow(y, _z2) % p % q);
        }

        public void CalculateZ(int s, int w, int q)
        {
            _z1 = s * _v % q;
            _z2 = (q - w) * _v % q;
        }

        public void CalculateH(string input)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                _h = new BigInteger(hash.Concat(new byte[] {0}).ToArray());
            }
        }
    }
}