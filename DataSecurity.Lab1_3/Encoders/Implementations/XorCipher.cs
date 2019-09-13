using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_3.Encoders.Interfaces;

namespace DataSecurity.Lab1_3.Encoders.Implementations
{
    class XorCipher : BaseEncoder, IEncoder
    {
        public string Name => "XOR cipher";

        public string Encrypt(string message)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string encryptedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
