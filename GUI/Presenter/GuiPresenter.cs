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

        public GuiPresenter( IView view, IModel model )
        {
            this.view = view;
            this.model = model;
            view.SetConnectionButtonText("Connect");
            view.SetAddFilesButton(false);
            view.SetUploadButton(false);
        }

        public Dictionary<string,List<string>> Connect()
        {
            var root_dir = model.Connect();

            view.SetConnectionButtonText("Disconnect");
            view.SetAddFilesButton(true);
            view.SetUploadButton(true);

            return root_dir;
        }

        public void Disconnect()
        {
            view.SetConnectionButtonText("Connect");
            view.ClearTree();
            view.SetAddFilesButton(false);
            view.SetUploadButton(false);
        }

        public void AddFileToUpload( List<string> hierarchy, string path_to_file)
        {
            model.AddFileToUpload(hierarchy, path_to_file);
        }

        public void Upload()
        {
            model.Upload();
        }
    }
}
