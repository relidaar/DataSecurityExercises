using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_6.Interfaces
{
    public interface IEncoder
    {
        string Name { get; }
        string Encrypt(string number);
        string Decrypt(string encryptedNumber);
    } 
}
