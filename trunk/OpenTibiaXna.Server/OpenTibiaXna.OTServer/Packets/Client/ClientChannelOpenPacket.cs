using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Client
{
    public class ClientChannelOpenPacket : PacketObject
    {
        public ChatChannel Channel { get; set; }

        public static ClientChannelOpenPacket Parse(NetworkMessageEngine message)
        {
            ClientChannelOpenPacket p = new ClientChannelOpenPacket();
            p.Channel = (ChatChannel)message.GetUInt16();
            return p;
        }
    }
}