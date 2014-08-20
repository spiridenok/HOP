using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using HOP.Storage.API;
using HOP.Storage.DropBox;
using HOP.Config;
using System.Collections.Generic;

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
            storage = new DropBoxStorage(new Configuration());
        }

        [ClassCleanup]
        public static void TearDown()
        {
            storage.CloseConnection();
        }

        [TestMethod]
        public void TestRootDir()
        {
            storage.OpenConnection();

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

        [TestMethod]
        public void TestUpload()
        {
            storage.OpenConnection();
            storage.ClearDir("/Test");
            storage.CreateDir("/Test/SubTest");

            var files_to_upload = new List<Tuple<List<string>, string>>();
            List<string> storage_dir = new []{"Test"}.ToList();
            files_to_upload.Add(new Tuple<List<string>, string>(storage_dir, "../../test.txt") );

            storage_dir = new List<string>{ "Test", "SubTest" };
            files_to_upload.Add(new Tuple<List<string>, string>(storage_dir, "../../test.exe"));

            storage.UploadFiles(files_to_upload);

            Assert.IsTrue( storage.GetDirListing("/Test").Contains( "test.txt" ) );
            Assert.IsTrue( storage.GetDirListing("/Test/SubTest").Contains( "test.exe" ) );
        }
    }
}
