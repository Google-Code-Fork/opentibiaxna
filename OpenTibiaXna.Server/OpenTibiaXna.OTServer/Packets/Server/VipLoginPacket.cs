using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class VipLoginPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message,uint id)
        {
            message.AddByte((byte)ServerPacketType.VipLogin);
            message.AddUInt32(id);
        }
    }
}

