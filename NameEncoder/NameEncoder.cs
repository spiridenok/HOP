using HOP.NameEncoder.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("NameEncoderTest")]

namespace HOP.NameEncoder
{
    class NameEncoder: INameEncoder
    {
        public NameEncoder()
        {
        }

        public string EncodeString(string str)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(str);

            for (byte i = 0; i < bytes.Length; i++ )
            {
                if (i % 2 == 0)
                {
                    bytes[i] += i;
                }
                else
                {
                    bytes[i] -= i;
                }
            }
            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        public string DecodeString(string str)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(str);

            for (byte i = 0; i < bytes.Length; i++)
            {
                if (i % 2 == 0)
                {
                    bytes[i] -= i;
                }
                else
                {
                    bytes[i] += i;
                }
            }
            return System.Text.Encoding.Unicode.GetString(bytes);
        }
    }
}
