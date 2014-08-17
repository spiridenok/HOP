using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HOP.GUI.Presenter.API;
using HOP.GUI.View.API;

namespace HOP.GUI.Presenter
{
    class GuiPresenter:IPresenter
    {
        private IView view;

        public GuiPresenter(IView view)
        {
            this.view = view;
        }

        public Dictionary<string,List<string>> Connect()
        {
            Dictionary<string, List<string>> root_dir = new Dictionary<string, List<string>>();
            List<string> sub_list = new List<string>();
            sub_list.Add( "1st sub root node" );
            root_dir.Add("1st root node", sub_list);
            root_dir.Add("2nd root node", null);

            view.SetConnectButtonState( false );

            return root_dir;
        }
    }
}
