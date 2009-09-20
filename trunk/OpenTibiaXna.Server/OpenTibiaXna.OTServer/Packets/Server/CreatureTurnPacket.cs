using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class CreatureTurnPacket : PacketObject
    {
        public static void Add
        (
            NetworkMessageEngine message,
            CreatureObject creature
        )
        {
            message.AddByte((byte)ServerPacketType.TileTransformThing);

            message.AddLocation(creature.Tile.Location);
            message.AddByte(creature.Tile.GetStackPosition(creature));
            message.AddUInt16(0x63);
            message.AddUInt32(creature.Id);
            message.AddByte((byte)creature.Direction);
        }
    }
}
