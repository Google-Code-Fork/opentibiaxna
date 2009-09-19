using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Entities
{
    public partial class MapTile
    {
        public static List<MapTile> GetAll()
        {
            return GenericDatabase.CurrentContext.CreateQuery<MapTile>(typeof(MapTile).Name).ToList();
        }
    }
}
