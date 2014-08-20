﻿using System;

using HOP.Storage.API;

using AppLimit.CloudComputing.SharpBox;
using System.IO;
using AppLimit.CloudComputing.SharpBox.StorageProvider.DropBox;

using HOP.Config.API;

using System.Runtime.CompilerServices;
using System.Collections.Generic;
[assembly: InternalsVisibleTo("DropBoxStorageTest")]

namespace HOP.Storage.DropBox
{
    class DropBoxStorage: IStorage
    {
        private CloudStorage dropBoxStorage;
        private string token_file_path;

        public DropBoxStorage(IConfiguration config)
        {
            token_file_path = config.GetTokenFilePath();
        }

        public void OpenConnection()
        {
             dropBoxStorage = new CloudStorage();

            ICloudStorageAccessToken accessToken = null;
            // load a valid security token from file
            using (FileStream fs = File.Open(token_file_path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }

            dropBoxStorage.Open(DropBoxConfiguration.GetStandardConfiguration(), accessToken);
        }

        public void CloseConnection()
        {
            dropBoxStorage.Close();
        }

        public IStorageDir GetRootDir()
        {
            return new DropBoxStorageDir( dropBoxStorage.GetRoot() );
        }

        public void UploadFiles(List<Tuple<List<string>, string>> files_to_upload)
        {
            foreach( var file in files_to_upload )
            {
                string drop_box_dir = "";
                foreach( var st in file.Item1 )
                {
                    // TODO: this needs to be cleaned up
                    if (!st.StartsWith("/") && st != "")
                        drop_box_dir += "/";
                    drop_box_dir += st;
                }

                dropBoxStorage.UploadFile( file.Item2, dropBoxStorage.GetFolder(drop_box_dir));
            }
        }

        public void ClearDir(string dir_name)
        {
            var storage_dir = dropBoxStorage.GetFolder("/" + dir_name);

            foreach( var el in GetDirListing( dir_name ) )
            {
                dropBoxStorage.DeleteFileSystemEntry( dropBoxStorage.GetFileSystemObject( el, storage_dir ) );
            }
        }

        public List<string> GetDirListing( string dir_name )
        {
            List<string> dir_list = new List<string>();
            var storage_dir = new DropBoxStorageDir( dropBoxStorage.GetFolder(dir_name) );

            foreach (var el in storage_dir.GetElements() )
            {
                dir_list.Add(el.GetName());
            }
            return dir_list;
        }

        public void CreateDir(string new_dir_name)
        {
            dropBoxStorage.CreateFolder(new_dir_name);
        }
    }
}
