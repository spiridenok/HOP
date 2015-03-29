using HOP.GUI.Model.API;
using HOP.Storage.API;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// TODO: this should be replaced by the interface & dep. injection
using HOP.NameEncoder;
using HOP.StorageObject.API;

namespace HOP.GUI.Model
{
    class GuiModel: IModel
    {
        IStorage storage;
        List<IStorageObject> files_to_upload;

        public GuiModel( IStorage storage )
        {
            this.storage = storage;
            files_to_upload = new List<IStorageObject>();
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
                var so = new StorageObject.StorageObject(storage_root_dir.GetName(), el.GetName(), el.IsDir() );
                root_dir.Add(so.getFilePath(), null);
            }

            return root_dir;
        }

        public void AddFileToUpload( List<string> hierarchy, string path_to_file)
        {
            files_to_upload.Add(new StorageObject.StorageObject(hierarchy, path_to_file));
        }

        public void Upload()
        {
            storage.UploadFiles(files_to_upload);
            files_to_upload = new List<IStorageObject>();
        }

        public bool IsDirectory(string name)
        {
            return storage.IsDirectory( name );
        }

        public void DownloadFile(List<string> storage_path, string file_path)
        {
            storage.DownloadFile(storage_path, file_path);
        }
    }
}
