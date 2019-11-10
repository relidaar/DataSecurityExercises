using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab2_4
{
    public class Ecc : IEncoder
    {
        private readonly int[,] _n;
        private readonly int _errorsCount;

        public Ecc(int errorsCount)
        {
            _n = CreateN();
            _errorsCount = errorsCount;
        }

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var binary = message.GetBinary().Substring(0, 11);
            var xr = binary.Select(d => d - '0').ToList();

            for (int i = 0; i < 4; i++)
            {
                int index = (int)(Math.Pow(2, i) - 1);
                xr.Insert(index, 0);
            }

            Console.WriteLine($"XR = {string.Join("", xr)}");

            var r = CalculateVector(xr.ToArray(), _n).ToArray();
            for (int i = 0; i < 4; i++)
            {
                int index = (int)(Math.Pow(2, i) - 1);
                xr.Insert(index, r[i]);
            }

            var pb = xr.Sum() % 2;

            Console.WriteLine($"pb = {string.Join("", pb)}");

            return binary + " " + string.Join("", r) + pb;
        }

        public string Decode(string encryptedMessage)
        {
            if (encryptedMessage == null) throw new NullReferenceException();

            var input = encryptedMessage.Split(' ');
            var xr = input[0].Select(d => d - '0').ToList();
            var r = input[1].Select(d => d - '0').ToArray();

            for (int i = 0; i < 4; i++)
            {
                int index = (int)(Math.Pow(2, i) - 1);
                xr.Insert(index, r[i]);
            }

            var rnd = new Random();
            for (int i = 0; i < _errorsCount; i++)
            {
                var index = rnd.Next(0, 11);
                xr[index] = xr[index] == 1 ? 0 : 1;
            }

            var pb = xr.Sum() % 2;

            Console.WriteLine($"XR' = {string.Join("", xr)}");
            Console.WriteLine($"pb' = {string.Join("", pb)}");

            var s = CalculateVector(xr.ToArray(), _n);

            return string.Join("", xr) + " " + string.Join("", s);
        }

        private static int[,] CreateN()
        {
            var n = new int[15, 4];
            for (int i = 0; i < 15; i++)
            {
                var num = Convert.ToString(i + 1, 2).PadLeft(4, '0').ToCharArray();
                Array.Reverse(num);
                for (int j = 0; j < 4; j++) n[i, j] = num[j] - '0';
            }

            return n;
        }

        private static IEnumerable<int> CalculateVector(int[] xr, int[,] n)
        {
            for (int j = 0; j < 4; j++)
            {
                int sum = 0;
                for (int i = 0; i < 15; i++)
                {
                    sum += xr[i] * n[i, j];
                }
                yield return sum % 2;
            }
        }
    }
}
