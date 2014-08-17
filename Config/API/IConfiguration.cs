namespace HOP.Config.API
{
    interface IConfiguration
    {
        string GetTokenFilePath();
        string GetKeyFilePath();
    }
}
