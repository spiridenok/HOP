using System;
using System.Collections.Generic;

using HOP.Configuartion.API;

// DropBoxStorage is not a real unit test, so it requires real configuration.
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("DropBoxStorageTest")]

namespace HOP.Configuartion
{
    class Configuration: IConfiguration
    {
        public string GetTokenFilePath()
        {
            return @"c:\Users\dspirydz\Documents\DropBox.Token";
        }

        public string GetKeyFilePath()
        {
            return @"c:\Users\dspirydz\Documents\TwoFish.Key";
        }
    }
}
