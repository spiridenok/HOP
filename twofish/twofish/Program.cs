using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ManyMonkeys.Cryptography;
using System.Security.Cryptography;

namespace twofish
{
    class Program
    {
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        static void Main(string[] args)
        {
            Twofish fish = new Twofish();

            fish.Mode = CipherMode.ECB;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] Key = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16};
            byte[] dummy = {};

            //create Twofish Encryptor from this instance
            ICryptoTransform encrypt = fish.CreateEncryptor(Key, dummy); // we use the plainText as the IV as in ECB mode the IV is not used

            //Create Crypto Stream that transforms file stream using twofish encryption
            CryptoStream cryptostream = new CryptoStream(ms, encrypt, CryptoStreamMode.Write);

            byte[] plainText = GetBytes("Some string to encrypt");

            //write out Twofish encrypted stream
            cryptostream.Write(plainText, 0, plainText.Length);

            cryptostream.Close();

            byte[] bytOut = ms.ToArray();

            System.Console.WriteLine( "Encrypted string: " + GetString( bytOut ) );

            //create Twofish Decryptor from our twofish instance
            ICryptoTransform decrypt = fish.CreateDecryptor(Key, plainText);

            System.IO.MemoryStream msD = new System.IO.MemoryStream();

            //create crypto stream set to read and do a Twofish decryption transform on incoming bytes
            CryptoStream cryptostreamDecr = new CryptoStream(msD, decrypt, CryptoStreamMode.Write);

            //write out Twofish encrypted stream
            cryptostreamDecr.Write(bytOut, 0, bytOut.Length);

            cryptostreamDecr.Close();

            byte[] bytOutD = msD.GetBuffer();

            System.Console.WriteLine("Decrypted string: " + GetString(bytOutD));
        }
    }
}
