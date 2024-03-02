namespace No1.Interfaces;

public interface IEncryption
{
    string Decrypt(string input);

    string Encrypt(string input);
}