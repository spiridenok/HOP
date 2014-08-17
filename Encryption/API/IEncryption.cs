namespace HOP.Encryption.API
{
    interface IEncryption
    {
        string Encrypt(string str);
        string Decrypt(string encrypted_str);

        // 'file' is not the best name for the parameter here...
        byte[] Encrypt(byte[] file);
        byte[] Decrypt(byte[] file);
    }
}
