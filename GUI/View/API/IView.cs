using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HOP.GUI.Presenter.API;

namespace HOP.GUI.View.API
{
    public interface IView
    {
        void SetPresenter(IPresenter presenter);
        void SetConnectButtonState( bool active );
    }
}
