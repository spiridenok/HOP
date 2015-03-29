using HOP.NameEncoder.API;
using System;
using System.Collections.Generic;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("NameEncoderTest")]

namespace HOP.NameEncoder
{
    class NameEncoder: INameEncoder
    {
        public NameEncoder()
        {
        }

        public string Encode(string str)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(str);

            for (byte i = 0; i < bytes.Length; i++ )
            {
                if (bytes[i] == 46)
                    break;
                if (i % 2 == 0)
                {
                    if (i % 3 == 0)
                    {
                        bytes[i] += i;
                    }
                    else
                    {
                        bytes[i] -= i;
                    }
                }
            }
            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        public string Decode(string str)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(str);

            for (byte i = 0; i < bytes.Length; i++)
            {
                if (i % 2 == 0)
                {
                    if (bytes[i] == 46 && bytes[i+1] == 0)
                        break;
                    if (i % 3 == 0)
                    {
                        bytes[i] -= i;
                    }
                    else
                    {
                        bytes[i] += i;
                    }
                }
            }
            return System.Text.Encoding.Unicode.GetString(bytes);
        }
    }
}
