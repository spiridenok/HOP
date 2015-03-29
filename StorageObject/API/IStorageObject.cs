using HOP.Config.API;
using System.Collections.Generic;

namespace HOP.StorageObject.API
{
    interface IStorageObject
    {
        List<string> getStorageDir();
        string getStoragePath();

        string getFilePath();
        string getEncryptedFilePath();

        string encryptFile(IConfiguration conf);
    }
}
