using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class TileRemoveThingPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, LocationEngine fromLocation, byte fromStackPosition)
        {
            if (fromStackPosition < 10)
            {
                message.AddByte((byte)ServerPacketType.TileRemoveThing);
                message.AddLocation(fromLocation);
                message.AddByte(fromStackPosition);
            }
        }
    }
}
