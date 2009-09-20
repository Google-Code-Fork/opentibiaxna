using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Client
{
    public class ChannelClosePacket : PacketObject
    {
        public ChatChannel Channel { get; set; }

        public static ChannelClosePacket Parse(NetworkMessageEngine message)
        {
            ChannelClosePacket p = new ChannelClosePacket();
            p.Channel = (ChatChannel)message.GetUInt16();
            return p;
        }
    }
}