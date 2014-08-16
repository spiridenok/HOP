namespace HOP.Encryption.API
{
    interface IEncryption
    {
        string Encrypt(string str);
        string Decrypt(string encrypted_str);
    }
}
