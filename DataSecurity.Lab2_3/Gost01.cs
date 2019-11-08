using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_3
{
    class Gost01Sender
    {
        public int R;
        public int S;

        public ECPoint P1;
        public ECPoint P2;

        private BigInteger _h = 19229;
        private int _d;
        private int _k;
        private int _e;
        private ECPoint _c;

        public Gost01Sender(ECPoint p) => P1 = p;

        public void GenerateKeys()
        {
            _d = new Random().Next(1, P1.Q);
            P2 = ECPoint.Multiply(P1, _d);
        }

        public void CalculateH(string input)
        {
            using (var md5 = MD5.Create())
            { 
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                _h = new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
            }
        }

        public void CalculatePointC() => _c = ECPoint.Multiply(P1, _k);

        public void CalculateE() => _e = (int) (_h % P1.Q);

        public int CalculateR() => R = _c.X % _c.Q;

        public int CalculateS() => S = (R * _d + _k * _e) % _c.Q;

        public void GenerateK() => _k = new Random().Next(1, P1.Q);
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
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                _h = new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
            }
        }

        public bool Check(int r) => r == _r;

        public void CalculateR() => _r = _c.X % _c.Q;

        public void CalculateE(int q) => _e = (int) (_h % q);

        public void CalculateV(int q) => _v = _e.GetInverse(q) % q;

        public void CalculateZ(int s, int r, int q)
        {
            _z1 = s * _v % q;
            _z2 = (q - r) * _v % q;
        }

        public void CalculatePointC(ECPoint p1, ECPoint p2) => 
            _c = ECPoint.Multiply(p1, _z1) + ECPoint.Multiply(p2, _z2);
    }
}
