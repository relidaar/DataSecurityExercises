using System;

namespace DataSecurity.Lab2_1
{
    public struct Digest
    {
        public uint A;
        public uint B;
        public uint C;
        public uint D;

        public Digest(uint a, uint b, uint c, uint d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public static Digest operator +(Digest d1, Digest d2)
        {
            return new Digest
            {
                A = (uint) ((d1.A + d2.A) % Math.Pow(2, 32)),
                B = (uint) ((d1.B + d2.B) % Math.Pow(2, 32)),
                C = (uint) ((d1.C + d2.C) % Math.Pow(2, 32)),
                D = (uint) ((d1.D + d2.D) % Math.Pow(2, 32))
            };
        }
    }
}