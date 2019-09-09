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
    }
}
