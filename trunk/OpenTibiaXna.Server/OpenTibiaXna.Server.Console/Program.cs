using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.Server.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SharpOT.Server server = new SharpOT.Server();
            server.Run();
        }
    }
}
