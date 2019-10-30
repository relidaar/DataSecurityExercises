namespace DataSecurity.Lab1_1.Encoders
{
    public static class Extensions
    {
        public static int Mod(this int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}