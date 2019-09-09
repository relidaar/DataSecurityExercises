using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_2.Encoders.Implementations;
using DataSecurity.Lab1_2.Encoders.Interfaces;

namespace DataSecurity.Lab1_2.Encoders
{
    class EncoderFactory
    {
        public IEncoder UseSimpleTranspositionCipher() => new SimpleTranspositionСipher();
        public IEncoder UseBlockTranspositionCipher(int blockSize = 3) => new BlockTranspositionCipher(blockSize);
        public IEncoder UseRouteCipher(int numberOfColumns = 3) => new RouteCipher(numberOfColumns);
        public IEncoder UseVerticalTranspositionCipher(string keyword = "secret") => new VerticalTranspositionCipher(keyword);
    }
}
