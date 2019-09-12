using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_6.Encoders.Implementations;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6.Encoders
{
    class EncoderFactory
    {
        public IEncoder UseDirectBinary() => new DirectBinary();
    }
}
