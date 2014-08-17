using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HOP.GUI.Presenter;
using HOP.GUI.Presenter.API;
using HOP.GUI.View.API;

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
            IPresenter presenter = new GuiPresenter( test_view );

            Assert.IsTrue(test_view.connect_button_active);
        }

        [TestMethod]
        // Connect must
        // - Perform connection to the storage
        // - Return listing of the root directory of the storage
        // - Disable connect button
        public void TestPresenterConenct()
        {
            TestView test_view = new TestView();
            IPresenter presenter = new GuiPresenter(test_view);

            presenter.Connect();

            Assert.IsFalse(test_view.connect_button_active);
        }
    }
}
