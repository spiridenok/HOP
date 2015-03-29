using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HOP.Storage.API;
using AppLimit.CloudComputing.SharpBox;

namespace HOP.Storage.DropBox
{
    class DropBoxStorageDir: IStorageDir
    {
        ICloudDirectoryEntry dir;

        public bool IsDir() { return true; }

        public DropBoxStorageDir(ICloudDirectoryEntry dir)
        {
            this.dir = dir;
        }

        public IStorageElement[] GetElements()
        {
            List<IStorageElement> elements = new List<IStorageElement>();

            // TODO: Use some kind of factory here...
            foreach (var el in dir)
            {
                if (el is ICloudDirectoryEntry)
                {
                    elements.Add(new DropBoxStorageDir((ICloudDirectoryEntry)el));
                }
                else if (el is ICloudFileSystemEntry)
                {
                    elements.Add( new DropBoxStorageFile((ICloudFileSystemEntry)el));
                }
            }

            return elements.ToArray();
        }

        public string GetName()
        {
            return dir.Name;
        }

    }
}