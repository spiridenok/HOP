using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HOP.GUI.Model;
using HOP.Storage.API;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace GuiTest
{
    [TestClass]
    public class ModelTest
    {
        class TestStorageDir : IStorageDir
        {
            public string name;
            public TestStorageDir(string name)
            {
                this.name = name;
            }

            public string GetName()
            {
                return name;
            }

            public static IStorageDir root_element = new TestStorageDir( "root" ); 
            public static IStorageDir second_element = new TestStorageDir( "second" ); 

            public IStorageElement[] GetElements()
            {
                return new IStorageElement[]{ root_element, second_element };
            }
        }

        class TestStorage : IStorage
        {
            public bool connected;

            public void OpenConnection()
            {
                connected = true;
            }
            public void CloseConnection()
            {
                connected = false;
            }

            public IStorageDir GetRootDir()
            {
                return new TestStorageDir("root dir");
            }

            public List<Tuple<List<string>, string>> files_to_upload;
            public void UploadFiles(List<Tuple<List<string>, string>> files_to_upload)
            {
                this.files_to_upload = files_to_upload;
            }

            public List<string> GetDirListing(string dir_name)
            {
                return null;
            }

            public void ClearDir(string dir) { }
            public void CreateDir(string dir) { }
            public bool IsDirectory(string node)
            {
                return node == "root dir";
            }

            public List<string> download_file;
            public void DownloadFile(List<string> file_path)
            {
                download_file = file_path;
            }
        }

        [TestMethod]
        // When connected the model must return the root dir of the storage
        public void TestConnect()
        {
            var test_storage = new TestStorage();
            var model = new GuiModel(test_storage);

            Dictionary<string, List<string>> root = model.Connect();

            Assert.IsTrue(test_storage.connected);

            Assert.AreEqual(2, root.Count);
            Assert.IsTrue(root.Keys.ToList().Exists( e => TestStorageDir.root_element.GetName().Equals( e ) ) );
            Assert.IsTrue(root.Keys.ToList().Exists( e => TestStorageDir.second_element.GetName().Equals( e ) ) );

            Assert.IsTrue( root.Values.ToList().TrueForAll( e => e == null ) );
        }

        // TODO: this test case is too big, it should be splitted in 3 different test cases
        [TestMethod]
        [Description("All added files must be uploaded")]
        public void TestUpload()
        {
            var test_storage = new TestStorage();
            var model = new GuiModel(test_storage);

            model.Connect();

            List<string> h_file_1 = new List<string>{ "hierarchy file one" };
            List<string> h_file_2 = new List<string>{ "hierarchy file two" };
            const string path_file_1 = "path file one";
            const string path_file_2 = "path file two";

            model.AddFileToUpload( h_file_1, path_file_1 );
            model.AddFileToUpload( h_file_2, path_file_2 );

            Assert.IsNull(test_storage.files_to_upload);

            model.Upload();

            Assert.AreEqual(2, test_storage.files_to_upload.Count );
            Assert.IsTrue(test_storage.files_to_upload.Exists(
                                        e => e.Item1.Equals( h_file_1 ) && e.Item2.Equals( path_file_1 ) ) );
            Assert.IsTrue(test_storage.files_to_upload.Exists(
                                        e => e.Item1.Equals( h_file_2 ) && e.Item2.Equals( path_file_2 ) ) );

            model.Upload();
            Assert.AreEqual(0, test_storage.files_to_upload.Count);
        }

        [TestMethod]
        [Description("It must be possible to distinguish between a directory or a regular file in the storage")]
        public void TestDirectoryOrFile()
        {
            var test_storage = new TestStorage();
            var model = new GuiModel(test_storage);

            model.Connect();

            Assert.IsTrue(model.IsDirectory(test_storage.GetRootDir().GetName()));
            Assert.IsFalse(model.IsDirectory("second") );
        }

        [TestMethod]
        [Description("Specified file must be downloaded to the right location")]
        public void TestDownloadFile()
        {
            var test_storage = new TestStorage();
            var model = new GuiModel(test_storage);

            model.Connect();

            List<string> file_to_download = new List<string> { "/one/two/three" };
            model.DownloadFile( file_to_download );
            Assert.AreEqual( file_to_download, test_storage.download_file );
        }
    }
}
