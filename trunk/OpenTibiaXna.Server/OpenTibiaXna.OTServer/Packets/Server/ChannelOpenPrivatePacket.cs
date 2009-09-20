using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class ChannelOpenPrivatePacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message,string Name)
        {
            message.AddByte((byte)ServerPacketType.ChannelOpenPrivate);
            message.AddString(Name);
        }

    }
}