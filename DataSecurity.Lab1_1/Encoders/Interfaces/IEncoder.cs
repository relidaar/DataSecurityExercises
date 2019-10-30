namespace DataSecurity.Lab1_1.Encoders.Interfaces
{
    public interface IEncoder
    {
        string Encode(string message);
        string Decode(string encryptedMessage);
    }
}