using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using DataSecurity.Extensions;
using Math = System.Math;

namespace DataSecurity.Lab2_6
{
    public class Smpc
    {
        public int E { get; private set; }
        public int N { get; private set; }

        private readonly int _value;
        private int _d;
        private int _x;

        public Smpc(int value) => _value = value;

        public void GenerateKeys()
        {
            var p = MathExtensions.GetPrime(_value);
            var q = MathExtensions.GetPrime(_value);

            N = p * q;
            var m = (p - 1) * (q - 1);

            _x = new Random().Next(1, _value);
            E = GetE(m);
            _d = E.GetInverse(m);
        }

        private int GetE(int m)
        {
            var rnd = new Random();
            var value = rnd.Next(1, _value);
            while (MathExtensions.GetNod(value, m) != 1) value = rnd.Next(1, _value);

            return value;
        }

        public BigInteger Encode(int e, int n) => BigInteger.Pow(_value + _x, e) % n;

        public BigInteger Encode(BigInteger value, int e, int n)
        {
            var decoded = BigInteger.Pow(value, _d) % N;
            return BigInteger.Pow(decoded + _value, e) % n;
        }

        public BigInteger Decode(BigInteger value) => BigInteger.Pow(value, _d) % N - _x;
    }
}
