using System;

using HOP.Storage.API;

using AppLimit.CloudComputing.SharpBox;
using System.IO;
using AppLimit.CloudComputing.SharpBox.StorageProvider.DropBox;

using HOP.Configuartion.API;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("DropBoxStorageTest")]

namespace HOP.Storage.DropBox
{
    class DropBoxStorage: IStorage
    {
        private CloudStorage dropBoxStorage;

        public void OpenConnection( IConfiguration config )
        {
             dropBoxStorage = new CloudStorage();

            ICloudStorageAccessToken accessToken = null;
            // load a valid security token from file
            using (FileStream fs = File.Open(config.GetTokenFilePath(), FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }

            dropBoxStorage.Open(DropBoxConfiguration.GetStandardConfiguration(), accessToken);
        }

        public void CloseConnection()
        {
            dropBoxStorage.Close();
        }
    }
}
