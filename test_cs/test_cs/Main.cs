using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlowFishCS;

using NUnit.Framework;

using ZeroMQ;

using System.Threading;

namespace test_cs
{
    class Global
    {
        static public int global_var = 0;
        static public Semaphore semaphore = new Semaphore(1, 1);
    }

    [TestFixture]
    class TestBlowfish
    //class main
    {
        [TestCase]
        public void RoundTripTest()
        {
            Console.WriteLine("Start");
            BlowFish b = new BlowFish("04B915BA43FEB5B6");
            string org_text = "The quick brown fox jumped over the lazy dog.";
            //Console.WriteLine(plainText);
            string cipherText = b.Encrypt_CBC(org_text);
            //Console.WriteLine(cipherText);
            string decoded_text = b.Decrypt_CBC(cipherText);
            //Console.WriteLine(plainText);
            Assert.AreEqual(org_text, decoded_text);
        }

        [TestCase]
        public void CorruptDecodeTest()
        {
            Console.WriteLine("Start");
            BlowFish b = new BlowFish("04B915BA43FEB5B6");
            string org_text = "The quick brown fox jumped over the lazy dog.";
            string cipherText = b.Encrypt_CBC(org_text);
            System.Text.StringBuilder corrupted_text = new System.Text.StringBuilder(cipherText);
            corrupted_text[3] = 'A';
            corrupted_text[10] = 'B';
            corrupted_text[50] = 'C';
            string decoded_text = b.Decrypt_CBC(corrupted_text.ToString());
            //Console.WriteLine(decoded_text);
            Assert.AreNotEqual(org_text, decoded_text);
            Console.WriteLine(System.Environment.GetEnvironmentVariable("Path"));
        }
    }

    class Test0mq
    {
        [TestCase]
        public void InitTest()
        {
            ZeroMQ.ZmqContext context = ZeroMQ.ZmqContext.Create();
        }
    }


    class TestThreads
    {
        class ThreadClass
        {
            public ThreadClass()
            {
                Global.semaphore.WaitOne();
            }

            public void Run()
            {
                Global.global_var++;
                Global.semaphore.Release();
            }
        }

        [TestCase]
        public void CreateThread()
        {
            Global.global_var = 0;
            ThreadClass my_thread = new ThreadClass();

            Thread thread = new Thread(new ThreadStart(my_thread.Run));
            thread.Start();
            Global.semaphore.WaitOne(1000);
            Assert.AreEqual(Global.global_var, 1);
        }
    }
}

public class MainClass
{
    static void Main()
    {
        //Console.WriteLine(System.Environment.GetEnvironmentVariable("Path"));
        var a = new test_cs.Test0mq();
        a.InitTest();
    }
}