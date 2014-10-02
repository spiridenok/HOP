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

        void AddFileToUpload( List<string> hierarchy, string path_to_file);

        // TODO: dirty hack - this function return true if upload, false if download
        bool LoadAction();

        void NodeSelected( List<string> path_to_file );
    }
}
