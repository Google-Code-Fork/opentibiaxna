using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class CreatureChangeOutfitPacket : Packet
    {
        public static void Add(NetworkMessage message, Creature creature)
        {
            message.AddByte((byte)ServerPacketType.CreatureOutfit);
            message.AddUInt32(creature.Id);
            message.AddOutfit(creature.Outfit);
        }
    }
}
