using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSecurity.Lab2_3
{
    class ECPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int A { get; }
        public int B { get; }
        public int N { get; }
        public int Q { get; }

        public ECPoint(int x, int y, int a, int b, int n, int q)
        {
            X = x;
            Y = y;
            A = a;
            B = b;
            N = n;
            Q = q;
        }

        public static ECPoint operator +(ECPoint p1, ECPoint p2)
        {
            var p = new ECPoint(0, 0, p1.A, p1.B, p1.N, p1.Q);

            var s = FindSlope(p1, p2);

            p.X = (s * s - p1.X - p2.X).Mod(p.N);
            p.Y = (s * (p1.X - p.X) - p1.Y).Mod(p.N);

            return p;
        }

        public static int FindSlope(ECPoint p1, ECPoint p2)
        {
            var lambda = 0;

            if (p1.X == p2.X)
            {
                while ((2 * p1.Y * lambda).Mod(p1.N) != (3 * p1.X * p1.X + p1.A).Mod(p1.N)) lambda++;
            }
            else
            {
                while ((lambda * (p2.X - p1.X)).Mod(p1.N) != (p2.Y - p1.Y).Mod(p1.N)) lambda++;
            }

            return lambda;
        }

        public static ECPoint Multiply(ECPoint p, int k)
        {
            ECPoint pk = null;
            var q = p;
            foreach (var digit in Convert.ToString(k, 2).Reverse())
            {
                if (digit == '1')
                {
                    if (pk == null) pk = q;
                    else pk += q;
                }

                q += q;
            }

            return pk;
        }
    }
}
