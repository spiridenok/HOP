using HOP.GUI.Model.API;
using HOP.Storage.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOP.GUI.Model
{
    class GuiModel: IModel
    {
        IStorage storage;
        List<Tuple<List<string>, string>> files_to_upload;

        public GuiModel( IStorage storage )
        {
            this.storage = storage;
            files_to_upload = new List<Tuple<List<string>,string>>();
        }

        public Dictionary<string, List<string>> Connect()
        {
            Dictionary<string, List<string>> root_dir = new Dictionary<string, List<string>>();
            //List<string> sub_list = new List<string>();
            //sub_list.Add("1st sub root node");
            //root_dir.Add("1st root node", sub_list);
            //root_dir.Add("2nd root node", null);

            storage.OpenConnection();
            IStorageDir storage_root_dir = storage.GetRootDir();

            foreach (var el in storage_root_dir.GetElements())
            {
                root_dir.Add(el.GetName(), null);
            }

            return root_dir;
        }

        public void AddFileToUpload( List<string> hierarchy, string path_to_file)
        {
            files_to_upload.Add(new Tuple<List<string>, string>(hierarchy, path_to_file));
        }

        public void Upload()
        {
            storage.UploadFiles(files_to_upload);
            files_to_upload = new List<Tuple<List<string>, string>>();
        }

        public bool IsDirectory(string name)
        {
            return storage.IsDirectory( name );
        }
    }
}
