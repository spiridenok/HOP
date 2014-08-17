using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HOP.Encryption.API;
using HOP.Encryption;
using HOP.Config.API;
using System.IO;

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
        private void TestFileEncryption( string file_extension )
        {
            string test_file_path = "../../test." + file_extension;

            IEncryption enc = new TwoFishEncryption(new TestConfiguration());

            byte[] test_file = File.ReadAllBytes(test_file_path);

            byte[] encrypted_file = enc.Encrypt(test_file);
            byte[] decrypted_file = enc.Decrypt(encrypted_file);

            File.WriteAllBytes("encrypted."+file_extension, encrypted_file);
            File.WriteAllBytes("decrypted."+file_extension, decrypted_file);

            Assert.IsFalse(encrypted_file.SequenceEqual(test_file));
            int org_len = decrypted_file.Length;
            Array.Resize(ref decrypted_file, test_file.Length);
            Assert.IsTrue(decrypted_file.SequenceEqual(test_file));
            Assert.IsTrue(test_file.Length - org_len < 10);
        }

        [TestMethod]
        public void TestStringEncryption()
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

        [TestMethod]
        public void TestJpegEncryption()
        {
            TestFileEncryption("jpg");
        }

        [TestMethod]
        public void TestPdfEncryption()
        {
            // This test does not use TestFileEncryption because it contains
            // some timing measurements for large files.
            
            // Comment this file out to get time measurements for a large file
            // string test_file_path = "../../test_big.pdf"
            string test_file_path = "../../test.pdf";

            IEncryption enc = new TwoFishEncryption(new TestConfiguration());

            Console.WriteLine("Read: {0}", DateTime.Now.ToString("HH:mm:ss tt"));
            byte[] test_file = File.ReadAllBytes(test_file_path);

            Console.WriteLine("Encrypt: {0}", DateTime.Now.ToString("HH:mm:ss tt"));
            byte[] encrypted_file = enc.Encrypt(test_file);
            Console.WriteLine("Decrypt: {0}", DateTime.Now.ToString("HH:mm:ss tt"));
            byte[] decrypted_file = enc.Decrypt(encrypted_file);

            Console.WriteLine("Write: {0}", DateTime.Now.ToString("HH:mm:ss tt"));
            File.WriteAllBytes("encrypted.pdf", encrypted_file);
            Console.WriteLine("Write: {0}", DateTime.Now.ToString("HH:mm:ss tt"));
            File.WriteAllBytes("decrypted.pdf", decrypted_file);

            Console.WriteLine("Compare: {0}", DateTime.Now.ToString("HH:mm:ss tt"));
            Assert.IsFalse(encrypted_file.SequenceEqual(test_file));
            Console.WriteLine("Compare: {0}", DateTime.Now.ToString("HH:mm:ss tt"));
            Assert.IsTrue(decrypted_file.SequenceEqual(test_file));
        }

        [TestMethod]
        public void TestTextEncryption()
        {
            TestFileEncryption("txt");
        }

        [TestMethod]
        public void TestMsWordEncryption()
        {
            TestFileEncryption("doc");
        }

        [TestMethod]
        public void TestExeEncryption()
        {
            TestFileEncryption("exe");
        }

        [TestMethod]
        public void TestZipEncryption()
        {
            TestFileEncryption("zip");
        }
        [TestMethod]
        public void TestExcelEncryption()
        {
            // Xlsx seems to have problems with padding zeros.
            // However Excel can successfully recover the file, so accept it for now.
            TestFileEncryption("xlsx");
        }
    }
}
