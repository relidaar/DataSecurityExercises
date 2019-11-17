using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_5
{
    class Dec
    {
        private readonly int[] _shifts = {1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1};

        private readonly int[] _p =
        {
            16, 7, 20, 21, 29, 12, 28, 17,
            1,15, 23, 26, 5, 18, 31, 10,
            2, 8, 24, 14, 32, 27, 3, 9,
            19, 13, 30, 6, 22, 11, 4, 25
        };

        private readonly string _kh;
        private readonly string _kl;

        public Dec(string key)
        { 
            var k = new string(_pc1.Select(index => key[index-1]).ToArray());
            _kh = k.Substring(0, 28);
            _kl = k.Substring(28, 28);
        }

        public string Encode(string input)
        {
            var t = new string(_ip.Select(index => input[index-1]).ToArray());
            Console.WriteLine($"IP: {t}\n");

            var h = t.Substring(0, 32);
            var l = t.Substring(32, 32);

            for (int i = 0; i < 16; i++)
            {
                var kt = CircularShift(_kh, _shifts[i]) + CircularShift(_kl, _shifts[i]);

                var k = new string(_pc2.Select(index => kt[index-1]).ToArray());

                h = h.Xor(CalculateF(l, k));

                if (i != 15) Swap(ref h, ref l);
            }

            t = h + l;

            var result = new string(_ip1.Select(index => t[index-1]).ToArray());
            return result;
        }

        private string CalculateF(string l, string k)
        {
            var x = new StringBuilder();
            x.Append(l[31] + l.Substring(0, 5));
            for (int i = 4; i < 28; i+=4) x.Append(l[i-1] + l.Substring(i, 5));
            x.Append(l[27] + l.Substring(28, 4) + l[0]);

            var h = x.ToString().Xor(k);
            var t = new StringBuilder();
            for (int i = 0; i < 48; i+=6)
            {
                var s = GenerateS();
                var current = h.Substring(i, 6);
                int n = Convert.ToInt32(current.Substring(0, 1) + 
                                        current.Substring(4, 1), 2).Mod(15);

                int m = Convert.ToInt32(current.Substring(1, 4), 2).Mod(15);

                var ti = Convert.ToString(s[n, m], 2).PadLeft(4, '0');
                t.Append(ti);
            }

            return new string(_p.Select(index => t[index-1]).ToArray());
        }

        private static int[,] GenerateS()
        {
            var s = new int[4, 15];

            var rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    s[i, j] = rnd.Next(0, 15);
                }
            }

            return s;
        }

        private static void Swap(ref string h, ref string l)
        {
            var temp = h;
            h = l;
            l = temp;
        }

        private static string CircularShift(string binary, int n) => 
            binary.Substring(n, binary.Length - n) + binary.Substring(0, n);

        #region IP

        private readonly int[] _ip =
        {
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };

        private readonly int[] _ip1 =
        {
            40, 8, 48, 16, 56, 24, 64, 32,
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25
        };

        #endregion

        #region PC

        private readonly int[] _pc1 =
        {
            57, 49, 41, 33, 25, 17, 9,
            1, 58, 50, 42, 34, 26, 18,
            10, 2, 59, 51, 43, 35, 27,
            19, 11, 3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
            7, 62, 54, 46, 38, 30, 22,
            14, 6, 61, 53, 45, 37, 29,
            21, 13, 5, 28, 20, 12, 4
        };

        private readonly int[] _pc2 =
        {
            14, 17, 11, 24, 1, 5,
            3, 28, 15, 6, 21, 10,
            23, 19, 12, 4, 26, 8,
            16, 7, 27, 20, 13, 2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };

        #endregion
    }
}
