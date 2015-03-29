using HOP.Config.API;
using HOP.StorageObject.API;
using System;
using System.Collections.Generic;

namespace HOP.Storage.API
{
    interface IStorage
    {
        void OpenConnection();

        void CloseConnection();

        void ClearDir(string dir_name);

        IStorageDir GetRootDir();

        void UploadFiles(List<IStorageObject> files_to_upload);

        List<IStorageObject> GetDirListing( string dir_name );

        void CreateDir(string new_dir_name);

        bool IsDirectory(string name);

        void DownloadFile(List<string> storage_path, string file_path);
    }
}
