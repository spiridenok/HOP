using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOP.NameEncoder.API
{
    interface INameEncoder
    {
        string Encode( string str );

        string Decode( string str );
    }
}
