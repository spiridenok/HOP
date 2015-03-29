using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using HOP.Storage.API;
using HOP.Storage.DropBox;
using HOP.Config;
using System.Collections.Generic;
using HOP.Config.API;
using HOP.StorageObject;
using HOP.StorageObject.API;

namespace DropBoxStorageTest
{
    public class TestConfiguration : IConfiguration
    {
        public string GetTokenFilePath()
        {
            return @"e:\HOP\DropBox.Test.Token";
        }

        public string GetKeyFilePath()
        {
            return @"..\..\TwoFish.Key";
        }
    }

    [TestClass]
    public class DropBoxStorageTest
    {
        static IStorage storage;

        [ClassInitialize]
        public static void SetUp( TestContext ctx )
        {
            // Create connection here because it's not a real unit test 
            // but uses real connection to the real drop box folder.
            storage = new DropBoxStorage(new TestConfiguration());
            storage.OpenConnection();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            storage.CloseConnection();
        }

        // TODO: check why this test fails when executed with other tests
        [TestMethod]
        public void TestUpload()
        {
            storage.ClearDir("/Test");
            storage.CreateDir("/Test/SubTest");

            List<string> storage_dir = new []{"Test"}.ToList();
            storage_dir = new List<string>{ "Test", "SubTest" };
            var f1 = new StorageObject(storage_dir, @"..\..\test.txt"); 
            var f2 = new StorageObject(storage_dir, @"..\..\test.exe");

            var files_to_upload = new List<IStorageObject>(){f1,f2};

            storage.UploadFiles(files_to_upload);

            // TODO: this test case creates temp files which should not be created at all.
            // Currently they are just ignored with .gitignore. 
            Assert.IsTrue(storage.GetDirListing("/Test").Exists( e => e.getStoragePath().Equals( f1.getEncryptedFilePath().Replace(@"..\..\", "/") ) ) );
            Assert.IsTrue(storage.GetDirListing("/Test/SubTest").Exists( e => e.getStoragePath().Equals( f2.getEncryptedFilePath().Replace(@"..\..\", "/") ) ) );
        }

        [TestMethod]
        public void TestRootDir()
        {
            IStorageDir root_dir = storage.GetRootDir();
            Assert.IsNotNull(root_dir);

            // Because we don't know how many elements there will be in the real root folder
            // (another reason to use a dummy drop box implementation or at least 
            // use a test storage which is different from the production storage)
            // we just assert here that there should be at least 1 directory and at least 1 test file.
            IStorageElement[] root_elements = root_dir.GetElements();

            Assert.IsTrue(root_elements.Any(el => (el is IStorageDir)));
            Assert.IsTrue(root_elements.Any(el => (el is IStorageFile)));
        }

        [TestMethod]
        public void TestDirectoryOrFile()
        {
            storage.ClearDir("/Test");
            storage.CreateDir("/Test/SubTest");

            var files_to_upload = new List<Tuple<List<string>, string>>();
            List<string> storage_dir = new[]{ "Test" }.ToList();
            files_to_upload.Add(new Tuple<List<string>, string>(storage_dir, "../../test.txt"));

            Assert.IsTrue( storage.IsDirectory("Test") );
            Assert.IsFalse(storage.IsDirectory("Test/test.txt"));
            Assert.IsTrue(storage.IsDirectory("Test/SubTest"));
        }
    }
}
