using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOP.GUI.Presenter.API
{
    public interface IPresenter
    {
        Dictionary<string,List<string>> Connect();
    }
}
