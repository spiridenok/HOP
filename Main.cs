using System;
using System.Collections.Generic;
using System.Text;

using HOP.NameEncoder;

namespace HOP
{
    class HOP
    {
        static void Main( )
        {
            var ne =  new NameEncoder.NameEncoder();
            ne.Encode("bla");
        }
    }
}
