using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Client
{
    public class PrivateChannelOpenPacket : PacketObject
    {
        public string Receiver { get; set; }

        public static PrivateChannelOpenPacket Parse(NetworkMessageEngine message)
        {
            PrivateChannelOpenPacket p = new PrivateChannelOpenPacket();
            p.Receiver = message.GetString();
            return p;   
        }
    }
}