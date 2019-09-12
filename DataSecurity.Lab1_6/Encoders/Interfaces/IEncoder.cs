namespace DataSecurity.Lab1_6.Encoders.Interfaces
{
    public interface IEncoder
    {
        string Name { get; }
        string Encrypt(string number);
        string Decrypt(string encryptedNumber);
    } 
}
