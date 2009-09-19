using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Entities
{
    public partial class Player
    {
        public Player()
        {
            this.PlayerObject = new PlayerObject((Gender)this.Gender, (Vocation)this.Vocation);
        }

        public PlayerObject PlayerObject { get; set; }
    }
}
