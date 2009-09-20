using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class CreatureLightPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, uint creatureId, byte lightLevel, byte lightColor)
        {
            message.AddByte((byte)ServerPacketType.CreatureLight);
            message.AddUInt32(creatureId);
            message.AddByte(lightLevel);
            message.AddByte(lightColor);
        }
    }
}
