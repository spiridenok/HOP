using HOP.Config.API;
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

        void UploadFiles( List< Tuple< List<string>, string > > files_to_upload );

        List<string> GetDirListing( string dir_name );

        void CreateDir(string new_dir_name);

        bool IsDirectory(string name);

        void DownloadFile(List<string> storage_path, string file_path);
    }
}
