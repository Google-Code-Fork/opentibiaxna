using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class TileAddCreaturePacket : PacketObject
    {
        public static void Add
        (
            NetworkMessageEngine message,
            LocationEngine location,
            byte stackPosition,
            CreatureObject creature,
            bool known,
            uint removeKnown
        )
        {
            message.AddByte((byte)ServerPacketType.TileAddThing);

            message.AddLocation(location);
            message.AddByte(stackPosition);
            message.AddCreature(creature, known, removeKnown);
        }
    }
}
