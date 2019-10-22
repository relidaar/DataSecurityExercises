using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab2_1.Encoder
{
    public class BlockBuilder
    {
        private readonly string _input;

        public BlockBuilder(string input) => _input = input;

        public string[] GetBlocks()
        {
            var inputBinary = ToBinary(_input);
            var normalizedInput = new StringBuilder(inputBinary).Append('1');

            while (normalizedInput.Length % 512 != 448) normalizedInput.Append('0');

            var lengthBinary = ToBinary(inputBinary.Length,64);

            normalizedInput.Append(lengthBinary.Substring(32, 32));
            normalizedInput.Append(lengthBinary.Substring(0, 32));

            var blocks = new List<string>();
            var text = normalizedInput.ToString();

            for (int i = 0; i < normalizedInput.Length; i+=512)
            {
                blocks.Add(text.Substring(i, 512));
            }

            return blocks.ToArray();
        }

        private static string ToBinary(string data, int width = 8)
        {
            var result = new StringBuilder();
            foreach (var c in data.ToCharArray())
            {
                result.Append(Convert.ToString(c, 2).PadLeft(width, '0'));
            }

            return result.ToString();
        }

        private static string ToBinary(int data, int width = 8) =>
            Convert.ToString(data, 2).PadLeft(width, '0');
    }
}
