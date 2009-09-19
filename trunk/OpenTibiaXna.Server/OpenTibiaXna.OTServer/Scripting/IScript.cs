using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Scripting
{
    public interface IScript
    {
        bool Start(Game game);
        bool Stop();
    }
}
