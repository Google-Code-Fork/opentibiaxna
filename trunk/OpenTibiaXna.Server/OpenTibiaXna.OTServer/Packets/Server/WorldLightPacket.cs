using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class WorldLightPacket : Packet
    {
        public static void Add(NetworkMessage message, byte lightLevel, byte lightColor)
        {
            message.AddByte((byte)ServerPacketType.WorldLight);
            message.AddByte(lightLevel);
            message.AddByte(lightColor);
        }
    }
}
