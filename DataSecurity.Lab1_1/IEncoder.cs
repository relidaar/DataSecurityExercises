using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_1
{
    public interface IEncoder
    {
        string EncoderName { get; set; }        
        string Encrypt(string message);
    }
}
