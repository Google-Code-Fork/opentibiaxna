using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class EffectPacket : Packet
    {
        public static void Add(NetworkMessage message, Effect effect, Location location)
        {
            message.AddByte((byte)ServerPacketType.Effect);
            message.AddLocation(location);
            message.AddByte((byte)effect);
        }
    }
}
