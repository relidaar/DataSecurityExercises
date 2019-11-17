using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_6
{
    public class SchamirSchemeOwner
    {
        public List<(int x, int y)> Parts { get; private set; }
        public int P { get; }

        private readonly int _s;
        private readonly int _a1;
        private readonly int _a2;

        public SchamirSchemeOwner(int s)
        {
            _s = s;
            P = MathExtensions.GetPrime(s + 100, s);

            var rnd = new Random();
            _a1 = rnd.Next(1, P);
            _a2 = rnd.Next(1, P);
        }

        public List<(int x, int y)> CalculateParts()
        {
            Parts = new List<(int, int)>();
            for (int i = 0; i < 5; i++)
            {
                var x = i + 1;
                var y = (int) ((_a2 * Math.Pow(x, 2) + _a1 * x + _s) % P);
                Parts.Add((x, y));
            }

            return Parts;
        }
    }

    public class SchamirSchemeMember
    {
        public List<(int x, int y)> Parts { get; }
        public Func<int, int> L { get; private set; }

        public SchamirSchemeMember(List<(int, int)> parts) => Parts = parts;

        public Func<int, int> GeneratePolynomial(int p)
        {
            int L1(int x) => (x - Parts[1].x) * (x - Parts[2].x) /
                             ((Parts[0].x - Parts[1].x) * (Parts[0].x - Parts[2].x));

            int L2(int x) => (x - Parts[0].x) * (x - Parts[2].x) /
                             ((Parts[1].x - Parts[0].x) * (Parts[1].x - Parts[2].x));

            int L3(int x) => (x - Parts[0].x) * (x - Parts[1].x) /
                             ((Parts[2].x - Parts[0].x) * (Parts[2].x - Parts[1].x));

            L = x => (Parts[0].y * L1(x) + Parts[1].y * L2(x) + Parts[2].y * L3(x)) % p;

            return L;
        }

        public int CalculateS() => L(0);
    }
}
