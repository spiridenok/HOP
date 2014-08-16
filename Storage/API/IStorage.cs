using HOP.Configuartion.API;

namespace HOP.Storage.API
{
    interface IStorage
    {
        void OpenConnection(IConfiguration config);

        void CloseConnection( );

        IStorageDir GetRootDir();
    }
}
