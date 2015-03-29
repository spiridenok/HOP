using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HOP.Storage.API;
using AppLimit.CloudComputing.SharpBox;

namespace HOP.Storage.DropBox
{
    class DropBoxStorageFile:IStorageFile
    {
        ICloudFileSystemEntry file;

        public DropBoxStorageFile( ICloudFileSystemEntry file )
        {
            this.file = file;
        }

        public string GetName()
        {
            var ne = new NameEncoder.NameEncoder();

            return ne.Decode(file.Name);
        }
    }
}
