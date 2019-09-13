using System;
using System.Collections.Generic;
using System.Text;

namespace DataSecurity.Lab1_6.Encoders.Interfaces
{
    class BaseEncoder
    {
        protected string GetBinary(int number)
        {
            if (number == 0) return "";

            int remainder = number % 2;
            number /= 2;

            return GetBinary(number) + remainder;
        }
    }
}
