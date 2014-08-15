using HOP.NameEncoder.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOP.NameEncoder
{
    class NameEncoder: INameEncoder
    {
        public string EncodeString(string str)
        {
            return "BOBO";
        }

        public string DecodeString(string str)
        {
            return "OBOB";
        }
    }
}
