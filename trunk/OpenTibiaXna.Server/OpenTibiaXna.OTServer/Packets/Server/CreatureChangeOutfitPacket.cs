using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class CreatureChangeOutfitPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, CreatureObject creature)
        {
            message.AddByte((byte)ServerPacketType.CreatureOutfit);
            message.AddUInt32(creature.Id);
            message.AddOutfit(creature.Outfit);
        }
    }
}
