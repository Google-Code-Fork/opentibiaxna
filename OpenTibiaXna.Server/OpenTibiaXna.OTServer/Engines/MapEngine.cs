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
            foreach (MapItem mapItem in MapItem.GetAllOrderByStackPosition())
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

            //SQLiteDataReader reader = selectMapItemsCommand.ExecuteReader(); 
            // select * from MapItem order by StackPosition

            //while (reader.Read())
            //{
            //    int x = reader.GetInt32(0) - 32000;
            //    int y = reader.GetInt32(1) - 32000;
            //    int z = reader.GetInt32(2);
            //    ushort id = (ushort)reader.GetInt16(4);
            //    byte extra = reader.GetByte(5);

            //    Tile tile = map.GetTile(x, y, z);
            //    if (tile != null)
            //    {
            //        ItemObject item = new ItemObject(id);
            //        item.Extra = extra;
            //        tile.Items.Add(item);
            //    }
            //}
            //reader.Close();
        }
    }
}
