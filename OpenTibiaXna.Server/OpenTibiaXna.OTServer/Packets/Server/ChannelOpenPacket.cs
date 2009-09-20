using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class ChannelOpenPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, ushort channelId, string channelName)
        {
            message.AddByte((byte)ServerPacketType.ChannelOpen);
            message.AddUInt16(channelId);
            message.AddString(channelName);
        }
    }
}