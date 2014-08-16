using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HOP.Encryption.API;

using ManyMonkeys.Cryptography;
using System.Security.Cryptography;
using HOP.Configuartion.API;

using System.Runtime.CompilerServices;
using System.IO;
[assembly: InternalsVisibleTo("EncryptionTest")]

namespace HOP.Encryption
{
    class TwoFishEncryption:IEncryption
    {
        private byte[] key;

        public TwoFishEncryption(IConfiguration config)
        {
            key = File.ReadAllBytes(config.GetKeyFilePath());
        }

        // TODO: next 2 functions should be replaced by Encoding variants.
        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars).Replace("\0", string.Empty);
        }

        public string Encrypt(string str)
        {
            Twofish fish = new Twofish();

            fish.Mode = CipherMode.ECB;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] dummy = { };

            //create Twofish Encryptor from this instance
            ICryptoTransform encrypt = fish.CreateEncryptor(key, dummy); // we use the plainText as the IV as in ECB mode the IV is not used

            //Create Crypto Stream that transforms file stream using twofish encryption
            CryptoStream cryptostream = new CryptoStream(ms, encrypt, CryptoStreamMode.Write);

            byte[] plainText = GetBytes(str);

            //write out Twofish encrypted stream
            cryptostream.Write(plainText, 0, plainText.Length);

            cryptostream.Close();

            byte[] bytOut = ms.ToArray();

            return GetString(bytOut);
        }

        public string Decrypt(string encrypted_str)
        {
            Twofish fish = new Twofish();

            fish.Mode = CipherMode.ECB;

            byte[] plainText = {};

            //create Twofish Decryptor from our twofish instance
            ICryptoTransform decrypt = fish.CreateDecryptor(key, plainText);

            System.IO.MemoryStream msD = new System.IO.MemoryStream();

            //create crypto stream set to read and do a Twofish decryption transform on incoming bytes
            CryptoStream cryptostreamDecr = new CryptoStream(msD, decrypt, CryptoStreamMode.Write);

            byte[] bytOut = GetBytes(encrypted_str);

            //write out Twofish encrypted stream
            cryptostreamDecr.Write(bytOut, 0, bytOut.Length);

            cryptostreamDecr.Close();

            byte[] bytOutD = msD.GetBuffer();

            return GetString(bytOutD);
        }
    }
}
