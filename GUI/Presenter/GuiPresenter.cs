using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HOP.GUI.Presenter.API;
using HOP.GUI.View.API;

using System.Runtime.CompilerServices;
using HOP.Storage.API;
using HOP.GUI.Model.API;
[assembly: InternalsVisibleTo("GuiTest")]

namespace HOP.GUI.Presenter
{
    class GuiPresenter:IPresenter
    {
        private IView view;
        private IModel model;

        // TODO: storage should be moved to the GUI model, presenter has nothing to do with that.
        public GuiPresenter( IView view, IModel model )
        {
            this.view = view;
            this.model = model;
            view.SetConnectButtonState(true);
        }

        public Dictionary<string,List<string>> Connect()
        {
            var root_dir = model.Connect();

            view.SetConnectButtonState( false );

            return root_dir;
        }
    }
}
