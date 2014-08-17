using HOP.Config.API;

namespace HOP.Storage.API
{
    interface IStorage
    {
        void OpenConnection();

        void CloseConnection( );

        IStorageDir GetRootDir();
    }
}
