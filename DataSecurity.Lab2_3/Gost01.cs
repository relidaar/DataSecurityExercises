using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_3
{
    public class Gost01Sender
    {
        private ECPoint _c;
        private int _d;
        private int _e;

        private BigInteger _h = 19229;
        private int _k;

        public ECPoint P1;
        public ECPoint P2;
        public int R;
        public int S;

        public Gost01Sender(ECPoint p)
        {
            P1 = p;
        }

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
                _h = new BigInteger(hash.Concat(new byte[] {0}).ToArray());
            }
        }

        public void CalculatePointC()
        {
            _c = ECPoint.Multiply(P1, _k);
        }

        public void CalculateE()
        {
            _e = (int) (_h % P1.Q);
        }

        public int CalculateR()
        {
            return R = _c.X % _c.Q;
        }

        public int CalculateS()
        {
            return S = (R * _d + _k * _e) % _c.Q;
        }

        public void GenerateK()
        {
            _k = new Random().Next(1, P1.Q);
        }
    }

    public class Gost01Receiver
    {
        private ECPoint _c;
        private int _e;
        private BigInteger _h = 19229;
        private int _r;
        private int _v;
        private int _z1, _z2;

        public void CalculateH(string input)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                _h = new BigInteger(hash.Concat(new byte[] {0}).ToArray());
            }
        }

        public bool Check(int r)
        {
            return r == _r;
        }

        public void CalculateR()
        {
            _r = _c.X % _c.Q;
        }

        public void CalculateE(int q)
        {
            _e = (int) (_h % q);
        }

        public void CalculateV(int q)
        {
            _v = _e.GetInverse(q) % q;
        }

        public void CalculateZ(int s, int r, int q)
        {
            _z1 = s * _v % q;
            _z2 = (q - r) * _v % q;
        }

        public void CalculatePointC(ECPoint p1, ECPoint p2)
        {
            _c = ECPoint.Multiply(p1, _z1) + ECPoint.Multiply(p2, _z2);
        }
    }
}