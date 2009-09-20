using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class SelfAppearPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, uint playerId, bool canReportBugs)
        {
            message.AddByte((byte)ServerPacketType.SelfAppear);
            message.AddUInt32(playerId);
            message.AddByte(0x32);
            message.AddByte(0);
            message.AddByte(Convert.ToByte(canReportBugs));
        }
    }
}
