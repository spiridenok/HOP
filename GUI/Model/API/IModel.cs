﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOP.GUI.Model.API
{
    interface IModel
    {
        // Return the listing of the root directory + 1st level 
        Dictionary<string, List<string>> Connect();

        void AddFileToUpload( List<string> hierarchy, string file_path);

        void Upload();

        bool IsDirectory(string name);

        void DownloadFile(List<string> storage_path, string file_path);
    }
}
