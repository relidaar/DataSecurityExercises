using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_2.Encoders.Interfaces
{
    public interface IEncoder
    {
        string Name { get; }
        string Encrypt(string message);
        string Decrypt(string encryptedMessage);
    }
}
