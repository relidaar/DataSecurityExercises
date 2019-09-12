using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSecurity.Lab1_6
{
    static class Extensions
    {
        public static string ReplaceAt(this string value, int index, char newchar)
        {
            if (value.Length <= index) return value;

            return string.Concat(value.Select((c, i) => i == index ? newchar : c));
        }
    }
}
