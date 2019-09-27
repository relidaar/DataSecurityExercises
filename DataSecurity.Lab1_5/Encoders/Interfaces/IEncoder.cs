namespace DataSecurity.Lab1_5.Encoders.Interfaces
{
    public interface IEncoder
    {
        string Name { get; }
        string Encrypt(string message);
        string Decrypt(string encryptedMessage);
    }
}
