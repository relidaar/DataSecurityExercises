using System;
using System.Collections.Generic;
using System.Text;
using DataSecurity.Lab1_1.Encoders.Implementations;
using DataSecurity.Lab1_1.Encoders.Interfaces;

namespace DataSecurity.Lab1_1.Encoders
{
    class EncoderFactory
    {
        public IEncoder UseCaesarCipher(int shift = 3) => new CaesarCipher(shift);
        public IEncoder UseSloganCipher(string keyword = "secret") => new SloganCipher(keyword);
        public IEncoder UsePolybiusSquare(string keyword = "") => new PolybiusSquare(keyword);
        public IEncoder UseTrithemiusCipher(int a = 2, int b = 5, int c = 3) => new TrithemiusCipher(a, b, c);
        public IEncoder UseVigenereCipher(string keyword = "secret") => new VigenereCipher(keyword);
        public IEncoder UsePlayfairCipher(string keyword = "secret") => new PlayfairСipher(keyword);
    }
}
