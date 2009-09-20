using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class MapDescriptionPacket : MapPacket
    {
        public static void Add(Connection connection, NetworkMessageEngine message, LocationEngine playerLocation)
        {
            message.AddByte((byte)ServerPacketType.MapDescription);
            message.AddLocation(playerLocation);
            AddMapDescription(
                connection,
                message,
                playerLocation.X - 8,
                playerLocation.Y - 6,
                playerLocation.Z,
                18,
                14
            );
        }
    }
}
