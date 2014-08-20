using HOP.Config.API;
using System;
using System.Collections.Generic;

namespace HOP.Storage.API
{
    interface IStorage
    {
        void OpenConnection();

        void CloseConnection( );

        IStorageDir GetRootDir();

        void UploadFiles( List< Tuple< List<string>, string > > files_to_upload );
    }
}
