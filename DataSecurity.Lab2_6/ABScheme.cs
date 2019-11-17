using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_6
{
    public class ABSchemeOwner
    {
        public int M { get; }
        public int P { get; }
        public List<int> D { get; }
        public List<int> K { get; private set; }

        private readonly int _s;
        private int _r;
        private int _si;

        public ABSchemeOwner(int s, List<int> d, int m)
        {
            _s = s;
            D = d;
            M = m;
            if (m >= d.Count) throw new Exception("m cannot be greater than n");
            P = MathExtensions.GetPrime(s + 100, s);
        }

        public void CalculateR()
        {
            var max = 1;
            for (int i = 0; i < M; i++) max *= D[i];
            max = (max - _s) / P;

            _r = new Random().Next(1, max);
            _si = _s + _r * P;
        }

        public List<int> CalculateK()
        {
            K = D.Select(d => _si % d).ToList();
            return K;
        }
    }

    public class ABSchemeMember
    {
        public (int d, int k)[] Parts { get; }
        public List<(int d, int di)> D { get; private set; }
        private int _d;

        public ABSchemeMember(params (int, int)[] parts) => Parts = parts;

        public List<(int d, int di)> CalculateD()
        {
            D = new List<(int d, int di)>();
            _d = Parts.Aggregate(1, (i, part) => i * part.d);

            foreach (var part in Parts)
            {
                var d = _d / part.d;
                var di = d.GetInverse(part.d);
                D.Add((d,di));
            }

            return D;
        }

        public int CalculateS(int p)
        {
            var s = 0;
            for (int i = 0; i < Parts.Length; i++) s += Parts[i].k * D[i].d * D[i].di;

            s %= _d;
            return s % p;
        }
    }
}
