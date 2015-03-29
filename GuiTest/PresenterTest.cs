using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HOP.GUI.Presenter;
using HOP.GUI.Presenter.API;
using HOP.GUI.View.API;
using HOP.Storage.API;
using HOP.GUI.Model.API;
using System.Collections.Generic;

namespace GuiTest
{
    class TestView: IView
    {
        public void SetPresenter(IPresenter presenter)
        {
        }

        public string connection_button_text;
        public void SetConnectionButtonText(string text)
        {
            connection_button_text = text;
        }
        public string load_button_text;
        public void SetLoadButtonText(string text)
        {
            load_button_text = text;
        }

        public bool tree_cleared = false;

        public void ClearTree()
        {
            tree_cleared = true;
        }

        public bool upload_button_enabled = true;
        public void SetUploadButton(bool enable)
        {
            upload_button_enabled = enable;
        }

        public bool add_files_button_enabled = true;
        public void SetAddFilesButton(bool enable)
        {
            add_files_button_enabled = enable;
        }

        public string GetFilePath(string default_name)
        {
            return "get" + default_name;
        }
    }

    class TestModel : IModel
    {
        public Dictionary<string, List<string>> Connect()
        {
            Dictionary<string, List<string>> root_dir = new Dictionary<string, List<string>>();
            List<string> sub_list = new List<string>();
            sub_list.Add("2.1");
            root_dir.Add("1.1", sub_list);
            root_dir.Add("1.2", null);

            return root_dir;
        }

        public List<string> hierarchy;
        public string file_path;

        public void AddFileToUpload( List<string> hierarchy, string file_path)
        {
            this.hierarchy = hierarchy;
            this.file_path = file_path;
        }

        public bool uploaded;
        public void Upload()
        {
            uploaded = true;
        }
        
        public bool IsDirectory(string name)
        {
            return name == "1.1";
        }

        public void DownloadFile(List<string> storage_path, string file_path)
        {
            this.hierarchy = storage_path;
        }
    }

    [TestClass]
    public class PresenterTest
    {
        [TestMethod]
        // After initialization:
        // - Presenter set correctly
        // - Connect button must be have text "Connected"
        // - "LoadAction button must have text "Encrypt and LoadAction"
        // - "LoadAction" and "Add files" must be disabled
        public void TestPresenterInitialization()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter( test_view, test_model );

            Assert.AreEqual("Connect", test_view.connection_button_text);
            Assert.AreEqual("Encrypt and LoadAction", test_view.load_button_text);
            Assert.IsFalse(test_view.add_files_button_enabled);
            Assert.IsFalse(test_view.upload_button_enabled);
        }

        [TestMethod]
        // Connect must
        // - Perform connection to the storage
        // - Return listing of the root directory of the storage
        // - Change button to "Disconnect"
        // - "LoadAction" and "Add files" must be enabled
        public void TestPresenterConnect()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter(test_view, test_model);

            var root_dir = presenter.Connect();

            Assert.AreEqual( "Disconnect", test_view.connection_button_text );

            CollectionAssert.AreEqual( new string[]{"1.1", "1.2"}, root_dir.Keys.ToArray() );         
            CollectionAssert.AreEqual(new List<string>{ "2.1" }, root_dir.Values.ElementAt(0));
            Assert.AreEqual(null, root_dir.Values.ElementAt(1));
            Assert.IsTrue(test_view.add_files_button_enabled);
            Assert.IsTrue(test_view.upload_button_enabled);
        }

        [TestMethod]
        // Disconnect must
        // - Disconnect from the storage
        // - Clear root directory tree
        // - Change button to "Connect"
        // - "LoadAction" and "Add files" must be disabled
        public void TestPresenterDisonnect()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter(test_view, test_model);

            presenter.Connect();
            presenter.Disconnect();

            Assert.AreEqual("Connect", test_view.connection_button_text);
            Assert.IsTrue(test_view.tree_cleared);
            Assert.IsFalse(test_view.add_files_button_enabled);
            Assert.IsFalse(test_view.upload_button_enabled);
        }

        [TestMethod]
        [Description("Adding file must pass the file name to the model")]
        public void TestPresenterAddFile()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter(test_view, test_model);

            presenter.Connect();
            string test_file_path = "some path";
            var test_hierarchy = new List<string>{"str1", "str2" };
            presenter.AddFileToUpload( test_hierarchy, test_file_path );

            Assert.AreEqual(test_file_path, test_model.file_path);
            Assert.AreEqual(test_hierarchy, test_model.hierarchy);
        }

        [TestMethod]
        [Description("If a directory is selected the files must be uploaded, if a file is selected - downloaded")]
        public void TestPresenterUpload()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter(test_view, test_model);

            presenter.NodeSelected(new List<string>{"1.1"});
            presenter.LoadAction();
            Assert.IsTrue(test_model.uploaded);

            test_model.uploaded = false;
            presenter.NodeSelected(new List<string>{"2.1"});
            presenter.LoadAction();
            Assert.IsFalse(test_model.uploaded);
        }

        [TestMethod]
        [Description("'Add' button must be enabled only for directories")]
        public void TestNodeTypeSelection()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter(test_view, test_model);

            presenter.Connect();

            presenter.NodeSelected(new List<string> { "2.1" });
            Assert.IsFalse(test_view.add_files_button_enabled);
            presenter.NodeSelected(new List<string> { "1.1" });
            Assert.IsTrue(test_view.add_files_button_enabled);
            presenter.NodeSelected(new List<string> { "1.2" });
            Assert.IsFalse(test_view.add_files_button_enabled);
        }

        [TestMethod]
        [Description("'LoadAction' for directories, 'Download' for files")]
        public void TestLoadButtonText()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter(test_view, test_model);

            presenter.Connect();

            presenter.NodeSelected(new List<string> { "2.1" });
            Assert.AreEqual("Decrypt and Download", test_view.load_button_text);
            presenter.NodeSelected(new List<string> { "1.1" });
            Assert.AreEqual("Encrypt and LoadAction", test_view.load_button_text);
            presenter.NodeSelected(new List<string> { "1.2" });
            Assert.AreEqual("Decrypt and Download", test_view.load_button_text);
        }

        [TestMethod]
        [Description("Selected file must be downloaded")]
        public void TestDownloadFile()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter(test_view, test_model);

            presenter.Connect();

            var test_file_path = new List<string> { "str1", "str2", "some file" };
            presenter.NodeSelected(test_file_path);
            presenter.LoadAction();

            Assert.AreEqual(test_file_path, test_model.hierarchy);
        }
    }
}
