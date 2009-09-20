using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class CreatureMovePacket : PacketObject
    {
        public static void Add
        (
            NetworkMessageEngine message,
            LocationEngine fromLocation,
            byte fromStackPosition,
            LocationEngine toLocation
        )
        {
            message.AddByte((byte)ServerPacketType.CreatureMove);

            message.AddLocation(fromLocation);
            message.AddByte(fromStackPosition);
            message.AddLocation(toLocation);
        }
    }
}
