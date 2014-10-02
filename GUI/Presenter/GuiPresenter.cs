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
        private List<string> node;
        private bool dir_selected = true;

        public GuiPresenter( IView view, IModel model )
        {
            this.view = view;
            this.model = model;
            view.SetConnectionButtonText("Connect");
            view.SetLoadButtonText("Encrypt and LoadAction");
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

        public bool LoadAction()
        {
            if (dir_selected)
            {
                model.Upload();
                return true;
            }
            else
            {
                view.GetFilePath(node[0]);
                model.DownloadFile(node);
                return false;
            }
        }

        public void NodeSelected(List<string> node_name)
        {
            //Console.WriteLine("Selected node {0} is dir", node_name, model.IsDirectory(node_name));
            node = node_name;
            if (model.IsDirectory(node_name[0]))
            {
                view.SetAddFilesButton(true);
                view.SetLoadButtonText("Encrypt and LoadAction");
                dir_selected = true;
            }
            else
            {
                view.SetAddFilesButton(false);
                view.SetLoadButtonText("Decrypt and Download");
                dir_selected = false;
            }
        }
    }
}
