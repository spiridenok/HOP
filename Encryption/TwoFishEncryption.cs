using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HOP.Encryption.API;

using ManyMonkeys.Cryptography;
using System.Security.Cryptography;
using HOP.Configuartion.API;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("EncryptionTest")]

namespace HOP.Encryption
{
    class TwoFishEncryption:IEncryption
    {
        public TwoFishEncryption(IConfiguration config)
        {

        }

        public string Encrypt(string str)
        {
            Twofish fish = new Twofish();

            fish.Mode = CipherMode.ECB;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] dummy = { };

            //create Twofish Encryptor from this instance
            ICryptoTransform encrypt = fish.CreateEncryptor(Key, dummy); // we use the plainText as the IV as in ECB mode the IV is not used

            return str;
        }

        public string Decrypt(string encrypted_str)
        {
            return encrypted_str;
        }
    }
}
