using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class OutfitWindowPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, PlayerObject player, IEnumerable<OutfitObject> outfits)
        {
            message.AddByte((byte)ServerPacketType.OutfitWindow);
            message.AddOutfit(player.Outfit);
            //TODO: send list of outfits
            message.AddByte((byte)outfits.Count());

            foreach (OutfitObject outfit in outfits)
            {
                message.AddUInt16((ushort)outfit.LookType);
                message.AddString(outfit.Name);
                message.AddByte(outfit.Addons);
            }
        }
    }
}
