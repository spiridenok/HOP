using System;

using System.Linq;

using HOP.Storage.API;

using AppLimit.CloudComputing.SharpBox;
using System.IO;
using AppLimit.CloudComputing.SharpBox.StorageProvider.DropBox;

using HOP.Config.API;

using System.Runtime.CompilerServices;
using System.Collections.Generic;
using HOP.Encryption;
using HOP.StorageObject.API;
[assembly: InternalsVisibleTo("DropBoxStorageTest")]

namespace HOP.Storage.DropBox
{
    class DropBoxStorage: IStorage
    {
        private CloudStorage dropBoxStorage;
        private string token_file_path;
        private IConfiguration conf;

        public DropBoxStorage(IConfiguration config)
        {
            token_file_path = config.GetTokenFilePath();
            conf = config;
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

        public void ClearDir(string dir_name)
        {
            var storage_dir = dropBoxStorage.GetFolder("/" + dir_name, throwException: false);

            if (storage_dir == null) return; // Dir does not exist yet, nothing to clear.

            foreach( var el in GetDirListing( dir_name ) )
            {
                //dropBoxStorage.DeleteFileSystemEntry( dropBoxStorage.GetFileSystemObject( el, storage_dir ) );
            }
        }

        public List<IStorageObject> GetDirListing( string dir_name )
        {
            var dir_list = new List<IStorageObject>();
            var storage_dir = new DropBoxStorageDir( dropBoxStorage.GetFolder(dir_name) );

            foreach (var el in storage_dir.GetElements() )
            {
                dir_list.Add(new StorageObject.StorageObject( storage_dir.GetName(), el.GetName(), el.IsDir() ));
            }
            return dir_list;
        }

        public void CreateDir(string new_dir_name)
        {
            dropBoxStorage.CreateFolder(new_dir_name);
        }

        public bool IsDirectory(string name)
        {
            var el = dropBoxStorage.GetFileSystemObject(name, dropBoxStorage.GetFolder("/"));
            return el is ICloudDirectoryEntry;
        }

        public void DownloadFile(List<string> storage_path, string file_path)
        {
            var enc = new TwoFishEncryption(conf);

            var ne = new NameEncoder.NameEncoder();
            var str = ne.Encode(storage_path[0]);

            string tmp = System.IO.Path.GetTempPath();

            // Download the file first
            dropBoxStorage.DownloadFile("/" + str, tmp);

            // Then decrypt it and save to the right dir
            byte[] decrypted_file = enc.Decrypt(File.ReadAllBytes(tmp+str));
            File.WriteAllBytes(file_path, decrypted_file);

            File.Delete(tmp + str);
        }

        public void UploadFiles(List<IStorageObject> files_to_upload)
        {
            foreach( var storage_object in files_to_upload )
            {
                dropBoxStorage.UploadFile(storage_object.encryptFile(conf), dropBoxStorage.GetFolder(storage_object.getStoragePath()));
            }
        }
    }
}
