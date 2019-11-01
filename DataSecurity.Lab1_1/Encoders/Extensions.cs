using System.Collections.Generic;

namespace DataSecurity.Lab1_1.Encoders
{
    public static class Extensions
    {
        public static int Mod(this int x, int m) => (x % m + m) % m;

        public static IEnumerable<char> GenerateAlphabet(int n, int shift)
        {
            for (int i = 0; i < n; i++) 
                yield return (char) (i + shift);
        }
    }
}