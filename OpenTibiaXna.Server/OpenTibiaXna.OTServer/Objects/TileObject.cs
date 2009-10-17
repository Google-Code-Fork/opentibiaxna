using System;
using System.Linq;
using System.Collections.Generic;
using OpenTibiaXna.OTServer.Items;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Objects
{
    public class TileObject
    {
        public LocationEngine Location { get; set; }
        public ItemObject Ground { get; set; }
        public List<ItemObject> Items { get; set; }
        public List<CreatureObject> Creatures { get; set; }

        public bool IsProtectionZone = false;
        public bool IsNoPvpZone = false;
        public bool IsPvpZone = false;
        public bool IsNoLogoutZone = false;
        public bool IsRefreshZone = false;

        public TileObject()
        {
            Items = new List<ItemObject>();
            Creatures = new List<CreatureObject>();
            IsWalkable = true;
        }

        public bool IsWalkable { get; set; }

        public FloorChangeDirection FloorChange
        {
            get
            {
                if (Ground.Info.FloorChange != FloorChangeDirection.None)
                {
                    return Ground.Info.FloorChange;
                }
                else
                {
                    foreach (ItemObject item in Items)
                    {
                        if (item.Info.FloorChange != FloorChangeDirection.None)
                        {
                            return item.Info.FloorChange;
                        }
                    }
                }
                return FloorChangeDirection.None;
            }
        }

        public IEnumerable<ItemObject> GetTopItems()
        {
            return Items.Where(i => i.GetOrder() < 4);
        }

        public IEnumerable<ItemObject> GetDownItems()
        {
            return Items.Where(i => i.GetOrder() > 4);
        }

        public ThingObject GetThingAtStackPosition(byte stackPosition)
        {
            int n = -1;

            if (Ground != null)
            {
                ++n;
                if (stackPosition == n)
                {
                    return Ground;
                }
            }

            if (Items.Count > 0)
            {
                foreach (ItemObject item in GetTopItems())
                {
                    n++;
                    if (stackPosition == n)
                    {
                        return item;
                    }
                }
            }

            if (Creatures.Count > 0)
            {
                foreach (CreatureObject creature in Creatures)
                {
                    ++n;
                    if (stackPosition == n)
                    {
                        return creature;
                    }
                }
            }

            if (Items.Count > 0)
            {
                foreach (ItemObject item in GetDownItems())
                {
                    n++;
                    if (stackPosition == n)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public byte GetStackPosition(ThingObject thing)
        {
            int n = -1;

            if (Ground != null)
            {
                if (thing == Ground)
                {
                    return 0;
                }
                ++n;
            }

            if (Items.Count > 0)
            {
                foreach (ItemObject item in GetTopItems())
                {
                    ++n;
                    if (thing == item)
                    {
                        return (byte)n;
                    }
                }
            }

            if (Creatures.Count > 0)
            {
                foreach (CreatureObject creature in Creatures)
                {
                    ++n;
                    if (thing == creature)
                    {
                        return (byte)n;
                    }
                }
            }

            if (Items.Count > 0)
            {
                foreach (ItemObject item in GetDownItems())
                {
                    ++n;
                    if (thing == item)
                    {
                        return (byte)n;
                    }
                }
            }

            throw new Exception("Thing not found in tile.");
        }
    }
}