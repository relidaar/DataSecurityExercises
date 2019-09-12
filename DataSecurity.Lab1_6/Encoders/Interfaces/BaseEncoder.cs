using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_6.Encoders.Interfaces
{
    class BaseEncoder
    {
        protected string GetBinary(int number, int quotient)
        {
            if (quotient == 0) return "";

            number = quotient;
            int remainder = number % 2;
            quotient = number / 2;

            return GetBinary(number, quotient) + remainder;
        }
    }
}
