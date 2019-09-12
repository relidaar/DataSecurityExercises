using System;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    class ReverseBinary : IEncoder
    {
        public string Name => "Reverse binary";

        public string Encrypt(string number)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string encryptedNumber)
        {
            throw new NotImplementedException();
        }

        private string GetBinary(int abs, int i)
        {
            throw new NotImplementedException();
        }
    }
}
