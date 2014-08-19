using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOP.GUI.Presenter.API
{
    public interface IPresenter
    {
        // Returns the root directory of the connected storage.
        Dictionary<string,List<string>> Connect();

        void Disconnect();
    }
}
