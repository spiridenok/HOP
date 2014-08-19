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

        public bool tree_cleared = false;

        public void ClearTree()
        {
            tree_cleared = true;
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
        // - Connect button must be have text "Connecte"
        // - Presenter set correctly
        public void TestPresenterInitialization()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter( test_view, test_model );

            Assert.AreEqual("Connect", test_view.connection_button_text);
        }

        [TestMethod]
        // Connect must
        // - Perform connection to the storage
        // - Return listing of the root directory of the storage
        // - Change button to "Disconnect"
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
        }

        [TestMethod]
        // Disconnect must
        // - Disconnect from the storage
        // - Clear root directory tree
        // - Change button to "Connect"
        public void TestPresenterDisonnect()
        {
            TestView test_view = new TestView();
            TestModel test_model = new TestModel();
            IPresenter presenter = new GuiPresenter(test_view, test_model);

            presenter.Connect();
            presenter.Disconnect();

            Assert.AreEqual("Connect", test_view.connection_button_text);
            Assert.IsTrue(test_view.tree_cleared);
        }

    }
}
