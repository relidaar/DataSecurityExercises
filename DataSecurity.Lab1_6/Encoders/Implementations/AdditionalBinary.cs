using System;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    class AdditionalBinary : IEncoder
    {
        public string Name => "Additional binary";

        public string Encrypt(string number)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string encryptedNumber)
        {
            throw new NotImplementedException();
        }
    }
}
