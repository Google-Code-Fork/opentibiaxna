using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Objects
{
    public class TownObject
    {
        public uint Id { get; private set; }
        public string Name { get; private set; }
        public LocationEngine TempleLocation { get; private set; }

        public TownObject(uint id, string name, LocationEngine templeLocation)
        {
            Id = id;
            Name = name;
            TempleLocation = templeLocation;
        }
    }
}
