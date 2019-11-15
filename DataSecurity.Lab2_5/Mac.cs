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
            var synchroBinary = new StringBuilder();
            for (int i = 1; i < 65; i++) synchroBinary.Append(i % 2);

            var result = synchroBinary.ToString().Xor(inputBinary);

            var dec = new Dec(input.Substring(0, 8).GetBinary());

            return dec.Encode(result);
        }
    }
}
