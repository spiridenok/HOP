using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HOP.NameEncoder;

namespace NameEncoderTest
{
    [TestClass]
    public class NameEncoderTest
    {
        [TestMethod]
        public void TestLatinName()
        {
            NameEncoder ne = new NameEncoder();
            const string test_string = "Test string";
            string encoded_string = ne.EncodeString(test_string);
            string decoded_string = ne.DecodeString(encoded_string);

            Console.WriteLine("test:'{0}', encoded:'{1}', decoded:'{2}'",
                test_string, encoded_string, decoded_string);

            Assert.AreNotEqual(test_string, encoded_string);
            Assert.AreNotEqual(encoded_string,decoded_string);
            Assert.AreEqual(test_string, decoded_string, false);
        }

        [TestMethod]
        public void TestCyrillicName()
        {
            NameEncoder ne = new NameEncoder();
            const string test_string = "Test строка";
            string encoded_string = ne.EncodeString(test_string);
            string decoded_string = ne.DecodeString(encoded_string);

            Console.WriteLine("test:'{0}', encoded:'{1}', decoded:'{2}'",
                test_string, encoded_string, decoded_string);

            Assert.AreNotEqual(test_string, encoded_string);
            Assert.AreNotEqual(encoded_string, decoded_string);
            Assert.AreEqual(test_string, decoded_string, false);
        }
    }
}
