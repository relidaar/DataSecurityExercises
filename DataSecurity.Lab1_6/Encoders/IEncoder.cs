namespace DataSecurity.Lab1_6.Encoders
{
    public interface IEncoder
    {
        string Encode(string number);
        string Decode(string encryptedNumber);
    }
}