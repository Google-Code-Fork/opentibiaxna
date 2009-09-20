using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class PlayerWalkCancelPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, Direction direction)
        {
            message.AddByte((byte)ServerPacketType.PlayerWalkCancel);
            message.AddByte((byte)direction);
        }
    }
}
