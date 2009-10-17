using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Items;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Objects
{
    public class MapObject
    {
        public List<TownObject> Towns { get; private set; }
        public string Description { get; set; }
        public string SpawnFile { get; set; }
        public string HouseFile { get; set; }

        Dictionary<LocationEngine, TileObject> tiles = new Dictionary<LocationEngine, TileObject>();
        LocationEngine defaultLocation = new LocationEngine(32097, 32205, 7);

        public MapObject()
        {
            Towns = new List<TownObject>();
        }

        public void Load()
        {
            OpenTibia.OtbmReader reader = new OpenTibia.OtbmReader(@"Data\map.otbm");
            reader.GetMapTiles(this);
        }

        public LocationEngine GetDefaultLocation()
        {
            return new LocationEngine(defaultLocation);
        }

        public TileObject GetTile(LocationEngine location)
        {
            if (tiles.ContainsKey(location))
            {
                return tiles[location];
            }
            else
            {
                return null;
            }
        }

        public TileObject GetTile(int x, int y, int z)
        {
            return GetTile(
                new LocationEngine(x, y, z)
            );
        }

        public bool SetTile(LocationEngine location, TileObject tile)
        {
            tile.Location = location;
            tiles[location] = tile;
            return true;
        }
    }
}
