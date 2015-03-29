using System;
using System.Collections.Generic;

using HOP.Config.API;

// DropBoxStorage is not a real unit test, so it requires real configuration.
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("DropBoxStorageTest")]

namespace HOP.Config
{
    class Configuration: IConfiguration
    {
        // todo: Don't use a file - make it built in to the application
        public string GetTokenFilePath()
        {
            return @"e:\HOP\DropBox.Token";
        }

        public string GetKeyFilePath()
        {
            return @"e:\HOP\EncryptionTest\TwoFish.Key";
        }
    }
}
