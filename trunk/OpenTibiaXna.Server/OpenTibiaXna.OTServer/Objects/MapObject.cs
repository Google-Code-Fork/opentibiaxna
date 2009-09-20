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
        public const int Size = 1024;

        TileObject[, ,] tiles = new TileObject[Size, Size, 14];

        private void FillTiles(ushort id)
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    TileObject tile = new TileObject();

                    ItemObject ground = new ItemObject(id);
                    tile.Ground = ground;

                    tile.Location = new LocationEngine(x, y, 7);

                    tiles[x, y, 7] = tile;
                }
            }
        }

        public void Load()
        {
            // FillTiles(4526);
            MapEngine.GetMapTiles(this);
            MapEngine.GetMapItems(this);
        }

        public TileObject GetTile(LocationEngine location)
        {
            return GetTile(
                location.X,
                location.Y,
                location.Z
            );
        }

        public TileObject GetTile(int x, int y, int z)
        {
            if (x < 0 || x >= Size ||
                y < 0 || y >= Size ||
                z < 0 || z >= 14)
            {
                return null;
            }

            return tiles[x, y, z];
        }

        public bool SetTile(LocationEngine location, TileObject tile)
        {
            if (location.X < 0 || location.X >= Size ||
                location.Y < 0 || location.Y >= Size ||
                location.Z < 0 || location.Z >= 14)
            {
                return false;
            }
            tile.Location = location;
            tiles[location.X, location.Y, location.Z] = tile;
            return true;
        }
    }
}
