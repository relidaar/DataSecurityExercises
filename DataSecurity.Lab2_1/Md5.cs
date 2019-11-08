using System;
using System.Collections.Generic;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab2_1
{
    class HashMd5 : IEncoder
    {
        private readonly List<int[]> _s = new List<int[]>
        {
            new[]{ 7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22 },
            new[]{ 5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20 },
            new[]{ 4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23 },
            new[]{ 6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21 }
        };

        private readonly List<Func<uint, uint, uint, uint>> _functions = new List<Func<uint, uint, uint, uint>>
        {
            (x, y, z) => (x & y) | (~x & z),
            (x, y, z) => (x & z) | (~z & y),
            (x, y, z) => x ^ y ^ z,
            (x, y, z) => y ^ (~z | x)
        };

        public string Encode(string input)
        {
            var blocks = new BlockBuilder(input).GetBlocks();
            var digest = new Digest(0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476);

            foreach (var block in blocks)
            {
                var temp = digest;

                for (var i = 0; i < 4; i++) digest = RunRound(_functions[i], digest, block, i);

                digest += temp;
            }

            return $"{digest.D:x8}{digest.C:x8}{digest.B:x8}{digest.A:x8}";
        }

        public string Decode(string encryptedMessage)
        {
            throw new NotImplementedException();
        }

        private Digest RunRound(Func<uint, uint, uint, uint> func, Digest digest, string block, int round)
        {
            var s = _s[round];
            int j = 0;
            for (int i = 0; i < block.Length; i+=32, j++)
            {
                var k = (uint) (Math.Pow(2, 32) * Math.Abs(Math.Sin(j + 16 * round)));
                var result = func(digest.B, digest.C, digest.D);
                var t = Convert.ToUInt32(block.Substring(i, 32), 2);
                
                digest.A = (uint) ((digest.A + result) % Math.Pow(2, 32));
                digest.A = (uint) ((digest.A + t) % Math.Pow(2, 32));
                digest.A = (uint) ((digest.A + k) % Math.Pow(2, 32));
                digest.A = RotLeft(digest.A, s[j]);
                digest.A = (uint)((digest.A + digest.B) % Math.Pow(2, 32));

                var temp = digest.D;
                digest.D = digest.C;
                digest.C = digest.B;
                digest.B = digest.A;
                digest.A = temp;
            }

            return digest;
        }

        private static uint RotLeft(uint value, int n)
        {
            const int bitSize = sizeof(uint) * 8;
            var mod = (value >> (bitSize - n)) & ((1 << n) - 1);
            return (uint) ((value << n) | mod);
        }
    }
}
