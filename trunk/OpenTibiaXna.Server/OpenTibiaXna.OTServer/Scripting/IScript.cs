using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Scripting
{
    public interface IScript
    {
        bool Start(GameObject game);
        bool Stop();
    }
}
