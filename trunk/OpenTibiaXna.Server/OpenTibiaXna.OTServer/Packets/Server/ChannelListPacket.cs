using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class ChannelListPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, List<Channel> channels)
        {
            message.AddByte((byte)ServerPacketType.ChannelList);
            message.AddByte((byte)channels.Count);

            foreach (var c in channels)
            {
                message.AddUInt16((ushort)c.Id);
                message.AddString(c.Name);
            }
        }
    }
}