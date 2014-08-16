using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using HOP.Storage.API;
using HOP.Storage.DropBox;
using HOP.Configuartion;

namespace DropBoxStorageTest
{
    [TestClass]
    public class DropBoxStorageTest
    {
        static IStorage storage;

        [ClassInitialize]
        public static void SetUp( TestContext ctx )
        {
            // Create connection here because it's not a real unit test 
            // but uses real connection to the real drop box folder
            storage = new DropBoxStorage();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            storage.CloseConnection();
        }

        [TestMethod]
        public void TestRootDir()
        {
            storage.OpenConnection(new Configuration());

            IStorageDir root_dir = storage.GetRootDir();
            Assert.IsNotNull(root_dir);

            // Because we dont know how many elements there will be in the real root folder
            // (another reason to use a dummy drop box implementation or at least 
            // use a test storage which is different from the production storage)
            // we just assert here that there should be at least 1 directory and at least 1 test file.
            IStorageElement[] root_elements = root_dir.GetElements();

            Assert.IsTrue(root_elements.Any(el => (el is IStorageDir)));
            Assert.IsTrue(root_elements.Any(el => (el is IStorageFile)));
        }
    }
}
