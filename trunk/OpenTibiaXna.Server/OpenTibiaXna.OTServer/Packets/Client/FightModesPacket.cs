using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Client
{
    public class FightModesPacket : PacketObject
    {
        public FightMode FightMode { get; set; }
        public bool ChaseMode { get; set; }
        public bool SafeMode { get; set; }


        public static FightModesPacket Parse(NetworkMessageEngine msg)
        {
            FightModesPacket p = new FightModesPacket();

            p.FightMode = (FightMode)msg.GetByte();
            p.ChaseMode =Convert.ToBoolean(msg.GetByte());
            p.SafeMode = Convert.ToBoolean(msg.GetByte());

            return p;
        }

    }
}