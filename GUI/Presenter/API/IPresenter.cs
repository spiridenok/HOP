using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOP.GUI.Presenter.API
{
    public interface IPresenter
    {
        // Return the listing of the root directory + 1st level 
        Dictionary<string,List<string>> Connect();
    }
}
