using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Client
{
    public class ChangeOutfitPacket : PacketObject
    {
        public OutfitObject Outfit { get; set; }

        public static ChangeOutfitPacket Parse(NetworkMessageEngine message)
        {
            ChangeOutfitPacket packet = new ChangeOutfitPacket();

            packet.Outfit = message.GetOutfit();

            return packet;
        }
    }
}
