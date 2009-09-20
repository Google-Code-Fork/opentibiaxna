using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new ServerEngine().Run();
        }
    }
}
