using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Packets.Client
{
    public class PrivateChannelOpenPacket : Packet
    {
        public string Receiver { get; set; }

        public static PrivateChannelOpenPacket Parse(NetworkMessage message)
        {
            PrivateChannelOpenPacket p = new PrivateChannelOpenPacket();
            p.Receiver = message.GetString();
            return p;   
        }
    }
}