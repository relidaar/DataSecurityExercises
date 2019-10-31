using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_2.Encoders.Interfaces
{
    public interface IEncoder
    {
        string Encode(string message);
        string Decode(string encryptedMessage);
    }
}
