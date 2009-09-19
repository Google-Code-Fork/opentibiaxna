using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class CreatureMovePacket : Packet
    {
        public static void Add
        (
            NetworkMessage message,
            Location fromLocation,
            byte fromStackPosition,
            Location toLocation
        )
        {
            message.AddByte((byte)ServerPacketType.CreatureMove);

            message.AddLocation(fromLocation);
            message.AddByte(fromStackPosition);
            message.AddLocation(toLocation);
        }
    }
}
