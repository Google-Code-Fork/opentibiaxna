using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class WorldLightPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, byte lightLevel, byte lightColor)
        {
            message.AddByte((byte)ServerPacketType.WorldLight);
            message.AddByte(lightLevel);
            message.AddByte(lightColor);
        }
    }
}
