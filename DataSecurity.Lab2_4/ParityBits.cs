using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSecurity.Extensions;
using DataSecurity.Interfaces;

namespace DataSecurity.Lab2_4
{
    public class ParityBits : IEncoder
    {
        private readonly bool _isEven;

        public ParityBits(bool isEven = true) => _isEven = isEven;

        public string Encode(string message)
        {
            if (message == null) throw new NullReferenceException();

            var result = new StringBuilder();

            var binaries = message.Select(c => ((int) c).GetBinary().PadLeft(8, '0')).ToList();

            foreach (var count in binaries.Select(binary => binary.Count(d => d == '1')))
            {
                char digit;
                if (count % 2 == 0) digit = _isEven ? '0' : '1';
                else digit = _isEven ? '1' : '0';

                result.Append(digit);
            }

            return result.ToString();
        }

        public string Decode(string encryptedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
