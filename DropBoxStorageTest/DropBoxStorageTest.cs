using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HOP.Storage.API;
using HOP.Storage.DropBox;
using HOP.Configuartion;

namespace DropBoxStorageTest
{
    [TestClass]
    public class DropBoxStorageTest
    {
        [TestMethod]
        public void TestOpenConnection()
        {
            IStorage st = new DropBoxStorage();

            st.OpenConnection(new Configuration());

            // TODO: add real tests here...

            st.CloseConnection();
        }
    }
}
