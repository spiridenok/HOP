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

        public bool connect_button_active = false;

        public void SetConnectButtonState(bool active)
        {
            connect_button_active = active;
        }
    }

    class TestStorageDir : IStorageDir
    {
        public string name;

        public TestStorageDir(string name)
        {
            this.name = name;
        }

        public IStorageElement[] GetElements()
        {
            return new TestStorageDir[] { new TestStorageDir(name + ".sub") };
        }

        public string GetName()
        {
            return name;
        }
    }

    class TestStorage : IStorage
    {
        public bool connection_open = false;

        public void OpenConnection()
        {
            connection_open = true;
        }

        public void CloseConnection()
        {
            connection_open = false;
        }

        public IStorageDir GetRootDir()
        {
            return null;
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
    }

    [TestClass]
    public class PresenterTest
    {
        [TestMethod]
        // After initialization:
        // - Connect button must be enabled
        // - Presenter set correctly
        public void TestPresenterInitialization()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter( test_view, test_model );

            Assert.IsTrue(test_view.connect_button_active);
        }

        [TestMethod]
        // Connect must
        // - Perform connection to the storage
        // - Return listing of the root directory of the storage
        // - Disable connect button
        public void TestPresenterConnect()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter(test_view, test_model);

            var root_dir = presenter.Connect();

            Assert.IsFalse(test_view.connect_button_active);

            CollectionAssert.AreEqual( new string[]{"1.1", "1.2"}, root_dir.Keys.ToArray() );         
            CollectionAssert.AreEqual(new List<string>{ "2.1" }, root_dir.Values.ElementAt(0));
            Assert.AreEqual(null, root_dir.Values.ElementAt(1));
        }
    }
}
