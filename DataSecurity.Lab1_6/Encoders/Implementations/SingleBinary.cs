using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DataSecurity.Lab1_6.Encoders.Interfaces;

namespace DataSecurity.Lab1_6.Encoders.Implementations
{
    class SingleBinary : BaseEncoder, IEncoder
    {
        public string Name => "Single binary";

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
