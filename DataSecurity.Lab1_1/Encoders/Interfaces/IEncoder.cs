namespace DataSecurity.Lab1_1.Encoders.Interfaces
{
    public interface IEncoder
    {
        string Name { get; }
        string Encrypt(string message);
        string Decrypt(string encryptedMessage);
    }
}
