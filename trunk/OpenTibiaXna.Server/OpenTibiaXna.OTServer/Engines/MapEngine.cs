using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.OTServer;
using OpenTibiaXna.OTServer.Items;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Engines
{
    public class MapEngine
    {
        public static void GetMapTiles(MapObject map)
        {
            foreach (MapTile mapTile in MapTile.GetAll())
            {
                TileObject newTile = new TileObject();
                int x = mapTile.X - 32000;
                int y = mapTile.Y - 32000;
                int z = mapTile.Z;
                newTile.Ground = new ItemObject((ushort)mapTile.GroundId);
                LocationEngine location = new LocationEngine(x, y, z);
                map.SetTile(location, newTile);
            }
        }

        public static void GetMapItems(MapObject map)
        {
            foreach (MapItem mapItem in MapItem.GetAll().OrderBy(o => o.StackPosition))
            {
                int x = mapItem.X - 32000;
                int y = mapItem.Y - 32000;
                int z = mapItem.Z;
                ushort id = (ushort)mapItem.StackPosition;
                byte extra = Convert.ToByte(mapItem.Extra);

                TileObject tile = map.GetTile(x, y, z);
                if (tile != null)
                {
                    ItemObject item = new ItemObject(id);
                    item.Extra = extra;
                    tile.Items.Add(item);
                }
            }
        }
    }
}
