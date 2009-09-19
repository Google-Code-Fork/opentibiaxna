using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class ChannelListPacket : Packet
    {
        public static void Add(NetworkMessage message, List<Channel> channels)
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