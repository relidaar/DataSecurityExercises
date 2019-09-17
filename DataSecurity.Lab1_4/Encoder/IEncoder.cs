namespace DataSecurity.Lab1_4.Encoder
{
    public interface IEncoder
    {
        string Name { get; }
        string Encrypt(string message);
        string Decrypt(string encryptedMessage);
    }
}
