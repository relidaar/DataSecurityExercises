using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_5
{
    public class Mac
    {
        public string Encode(string input)
        {
            var inputBinary = input.Substring(0, 8).GetBinary();

            Console.WriteLine($"Message binary: {inputBinary}\n");
            var subkeyBinary = new StringBuilder();
            for (int i = 1; i < 65; i++) subkeyBinary.Append(i % 2);

            var xor = subkeyBinary.ToString().Xor(inputBinary);
            Console.WriteLine($"Subkey XOR binary: {xor}\n");

            var dec = new Dec(input.Substring(0, 8).GetBinary());

            return dec.Encode(xor);
        }
    }
}
