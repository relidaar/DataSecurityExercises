using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Extensions;

namespace DataSecurity.Lab2_1
{
    public class BlockBuilder
    {
        private readonly string _input;

        public BlockBuilder(string input)
        {
            _input = input;
        }

        public string[] GetBlocks()
        {
            var inputBinary = _input.GetBinary();
            var normalizedInput = new StringBuilder(inputBinary).Append('1');

            while (normalizedInput.Length % 512 != 448) normalizedInput.Append('0');

            var lengthBinary = Convert.ToString(inputBinary.Length, 2).PadLeft(64, '0');

            normalizedInput.Append(lengthBinary.Substring(32, 32));
            normalizedInput.Append(lengthBinary.Substring(0, 32));

            var blocks = new List<string>();
            var text = normalizedInput.ToString();

            for (var i = 0; i < normalizedInput.Length; i += 512)
                blocks.Add(text.Substring(i, 512));

            return blocks.ToArray();
        }
    }
}