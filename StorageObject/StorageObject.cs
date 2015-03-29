using HOP.Encryption;
using HOP.StorageObject.API;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using HOP.Config.API;
using System.IO;

[assembly: InternalsVisibleTo("DropBoxStorageTest")]

namespace HOP.StorageObject
{
    class StorageObject : IStorageObject
    {
        private List<string> storage_dir;
        private string file_path;
        private string encrypted_file_path;

        public StorageObject(List<string> storage_dir, string file_path)
        {
            this.file_path = file_path;
            this.storage_dir = storage_dir;
            encrypted_file_path = null;
        }

        public StorageObject(string storage_dir, string storage_file_name, bool is_dir)
        {
            if (!is_dir)
            {
                var ne = new NameEncoder.NameEncoder();
                this.file_path = ne.Decode(storage_file_name);
            }
            else
            {
                this.file_path = storage_file_name;
            }
            this.storage_dir = new List<string>(){storage_file_name};
            encrypted_file_path = null;
        }

        public List<string> getStorageDir()
        {
            return storage_dir;
        }

        public string getStoragePath()
        {
            string storage_path = "";
            foreach (var st in storage_dir)
            {
                // TODO: this needs to be cleaned up
                if (!st.StartsWith("/") && st != "")
                    storage_path += "/";
                storage_path += st;
            }
            return storage_path;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false:
            if ((object)obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            StorageObject o = obj as StorageObject;
            if ((System.Object)o == null)
            {
                return false;
            }
            return this.storage_dir.Equals(o.storage_dir) && this.file_path.Equals(o.file_path);
        }

        public override int GetHashCode()
        {
            return file_path.GetHashCode();
        }

        public string getFilePath()
        {
            return file_path;
        }

        public string getEncryptedFilePath()
        {
            var ne = new NameEncoder.NameEncoder();
            // Obfuscate the name
            var file_name = file_path.Split('\\').Last();
            var str = ne.Encode(file_name);
            encrypted_file_path = file_path.Replace(file_name, str);

            System.IO.File.Copy(file_path, encrypted_file_path, overwrite:true);

            // Encrypt the file
            //var enc = new TwoFishEncryption(conf);
            //byte[] encrypted_file = enc.Encrypt(File.ReadAllBytes(file.Item2));
            //File.WriteAllBytes(new_file_path, encrypted_file);
            return encrypted_file_path;
        }

        public string encryptFile(IConfiguration conf)
        {
            var enc = new TwoFishEncryption(conf);
            byte[] encrypted_file = enc.Encrypt(File.ReadAllBytes(getEncryptedFilePath()));
            File.WriteAllBytes(encrypted_file_path, encrypted_file);

            return encrypted_file_path;
        }
    }
}
