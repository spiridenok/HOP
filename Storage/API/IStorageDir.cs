using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOP.Storage.API
{
    interface IStorageDir:IStorageElement
    {
        IStorageElement[] GetElements();
    }
}
