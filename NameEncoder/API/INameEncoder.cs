using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOP.NameEncoder.API
{
    interface INameEncoder
    {
        string EncodeString( string str );

        string DecodeString( string str );
    }
}
