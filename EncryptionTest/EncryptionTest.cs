using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HOP.Encryption.API;
using HOP.Encryption;
using HOP.Configuartion.API;

namespace EncryptionTest
{
    public class TestConfiguration:IConfiguration
    {
        public string GetTokenFilePath()
        {
            throw new NotImplementedException();
        }

        public string GetKeyFilePath()
        {
            // This is not the file with the real key, just something for testing...
            return "../../TwoFish.Key";
        }
    }

    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string test_string = "Some string to encrypt";

            IEncryption enc = new TwoFishEncryption( new TestConfiguration() );

            string encrypted_string = enc.Encrypt(test_string);
            string decrypted_string = enc.Decrypt(encrypted_string);

            Console.WriteLine("test string='{0}', encrypted string='{1}', decrypted string='{2}'", 
                test_string, encrypted_string, decrypted_string);

            Assert.AreNotEqual(test_string, encrypted_string);
            Assert.AreNotEqual(decrypted_string, encrypted_string);
            Assert.AreEqual(test_string, decrypted_string);
        }
    }
}
