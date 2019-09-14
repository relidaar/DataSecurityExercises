using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_3.Encoders.Implementations;
using DataSecurity.Lab1_3.Encoders.Interfaces;

namespace DataSecurity.Lab1_3.Encoders
{
    class EncoderFactory
    {
        public IEncoder UseGammaCipher(int gammaCount = 10) => new GammaCipher(gammaCount); 
        public IEncoder UseXorCipher(int gammaCount = 10) => new XorCipher(gammaCount);
    }
}
