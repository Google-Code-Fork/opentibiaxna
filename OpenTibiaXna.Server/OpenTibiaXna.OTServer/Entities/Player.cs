using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Entities
{
    public partial class Player
    {
        public Player()
        {
            this.PlayerObject = new PlayerObject();

            this.PlayerObject = new PlayerObject();
            this.PlayerObject.Name = this.Name;
            this.PlayerObject.Id = (uint)this.PlayerId;
            this.PlayerObject.MaxHealth = (ushort)this.MaxHealth;
            this.PlayerObject.MaxMana = (ushort)this.MaxMana;
            this.PlayerObject.Outfit.LookType = (ushort)this.OutfitLookType;
            this.PlayerObject.Outfit.Head = Convert.ToByte(this.OutfitHead);
            this.PlayerObject.Outfit.Body = Convert.ToByte(this.OutfitBody);
            this.PlayerObject.Outfit.Legs = Convert.ToByte(this.OutfitLegs);
            this.PlayerObject.Outfit.Feet = Convert.ToByte(this.OutfitFeet);
            this.PlayerObject.Outfit.Addons = Convert.ToByte(this.OutfitAddons);

            if (this.LocationX.HasValue)
            {
                int x = this.LocationX.Value;
                int y = this.LocationY.Value;
                int z = this.LocationZ.Value;
                this.SavedLocation = new LocationEngine(x, y, z);
                this.PlayerObject.Direction = (Direction)this.Direction;
            }

            this.PlayerObject.Speed = (ushort)(220 + (2 * (this.Level - 1)));
        }

        public PlayerObject PlayerObject { get; set; }

        public ConnectionEngine Connection { get; set; }
        public LocationEngine SavedLocation { get; set; }
        public List<Channel> ChannelList { get; set; }
        public List<Channel> OpenedChannelList { get; set; }
        public FightMode FightMode { get; set; }
        public bool ChaseMode { get; set; }
        public bool SafeMode { get; set; }
        public Dictionary<uint, VipObject> VipList { get; set; }
        public int LastYellTime { get; set; }
    }
}
