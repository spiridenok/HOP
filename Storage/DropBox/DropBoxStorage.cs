using System;

using HOP.Storage.API;

using AppLimit.CloudComputing.SharpBox;
using System.IO;
using AppLimit.CloudComputing.SharpBox.StorageProvider.DropBox;

using HOP.Config.API;

using System.Runtime.CompilerServices;
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
    }
}
