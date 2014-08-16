using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace TwoFishKeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Need 1 string with numbers divided by comma. Exiting...");
                return;
            }

            string[] key = args[0].Split(',');

            if (key.Length != 16)
            {
                Console.WriteLine("There must be exactly 16 numbers in the input string, found {0}. Exiting...",
                    key.Length);
                return;
            }

            List<byte> result_key = new List<byte>(); 

            foreach( string k in key )
            {
                byte result = 0;
                if (Byte.TryParse(k, out result))
                {
                    result_key.Add(result);
                }
                else
                {
                    Console.WriteLine("Can not convert element '{0}' to byte. Exiting...", k);
                    return;
                }
            }

            const string file_name = "TwoFish.Key";
            Console.WriteLine("Writing array of keys to the output file '{0}' in the current directory",  file_name);
            File.WriteAllBytes( file_name, result_key.ToArray() );
            Console.WriteLine("Completed!");

            Console.WriteLine("Self check - trying to read the key from the file...");
            byte[] written_key = File.ReadAllBytes(file_name);

            if (!written_key.SequenceEqual(result_key.ToArray()))
            {
                Console.WriteLine("Write has failed! Expected {0}, got {1}",
                    Encoding.UTF8.GetString(result_key.ToArray()),
                    Encoding.UTF8.GetString(written_key));
            }
            else
            {
                Console.WriteLine("File is written correctly!");
            }
        }
    }
}
