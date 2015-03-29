using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HOP.NameEncoder;
using HOP.NameEncoder.API;

namespace NameEncoderTest
{
    [TestClass]
    public class NameEncoderTest
    {
        [TestMethod]
        public void TestLatinName()
        {
            INameEncoder ne = new NameEncoder();
            const string test_string = "Test string";
            string encoded_string = ne.Encode(test_string);
            string decoded_string = ne.Decode(encoded_string);

            Console.WriteLine("test:'{0}', encoded:'{1}', decoded:'{2}'",
                test_string, encoded_string, decoded_string);

            Assert.AreNotEqual(test_string, encoded_string);
            Assert.AreNotEqual(encoded_string,decoded_string);
            Assert.AreEqual(test_string, decoded_string, false);
        }

        [TestMethod]
        public void TestCyrillicName()
        {
            INameEncoder ne = new NameEncoder();
            const string test_string = "Тестовая строка";
            string encoded_string = ne.Encode(test_string);
            string decoded_string = ne.Decode(encoded_string);

            Console.WriteLine("test:'{0}', encoded:'{1}', decoded:'{2}'",
                test_string, encoded_string, decoded_string);

            Assert.AreNotEqual(test_string, encoded_string);
            Assert.AreNotEqual(encoded_string, decoded_string);
            Assert.AreEqual(test_string, decoded_string, false);
        }

        [TestMethod]
        public void TestEncodeTillDot()
        {
            INameEncoder ne = new NameEncoder();
            const string test_string = "Test строка.OK";
            string encoded_string = ne.Encode(test_string);
            string decoded_string = ne.Decode(encoded_string);

            Console.WriteLine("test:'{0}', encoded:'{1}', decoded:'{2}'",
                test_string, encoded_string, decoded_string);

            Assert.AreEqual(encoded_string.Split('.')[1], "OK", false);
            Assert.AreEqual(test_string, decoded_string, false);
        }
    }
}
