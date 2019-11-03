using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace DataSecurity.Lab2_3.Encoders
{
    public class RsaSender
    {
        public int e;
        public int n;
        public BigInteger s;

        private int _d;
        private BigInteger _h = 19229;

        private readonly int _lowerBound;
        private readonly int _upperBound;

        public RsaSender(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public void CalculateH(string input)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            _h = new BigInteger(hash.Concat(new byte[] { 0 }).ToArray()) % n;
            Console.WriteLine($"h = {_h}");
        }

        public BigInteger CalculateS() => s = BigInteger.Pow(_h, _d) % n;

        public void GenerateKeys()
        {
            var p = Extensions.GetPrime(_upperBound, _lowerBound);
            var q = Extensions.GetPrime(_upperBound, _lowerBound);

            n = p * q;

            int m = (p - 1) * (q - 1);
            e = GetE(m);
            _d = GetD(m);

            Console.WriteLine($"p = {p}, q = {q}, n = {n}, m = {m}, e = {e}, d = {_d}");
        }

        private int GetD(int m)
        {
            int value = 0;
            while (value * e % m != 1) value++;

            return value;
        }

        private int GetE(int m)
        {
            var rnd = new Random();
            int value = rnd.Next(_lowerBound, _upperBound);
            while (Extensions.GetNod(value, m) != 1) value = rnd.Next(_lowerBound, _upperBound);

            return value;
        }
    }

    public class RsaReceiver
    {
        private BigInteger _h = 19229;

        public void CalculateH(string input, int n)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            _h = new BigInteger(hash.Concat(new byte[] { 0 }).ToArray()) % n;
        }

        public bool Check(BigInteger s, int e, int n) => _h == BigInteger.Pow(s, e) % n;
    }
}
