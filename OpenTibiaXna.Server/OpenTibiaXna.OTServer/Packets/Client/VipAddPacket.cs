using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Packets.Client
{
    public class VipAddPacket
    {
        public string Name { get; set; }

        public static VipAddPacket Parse(NetworkMessageEngine message)
        {
            VipAddPacket p = new VipAddPacket();

            p.Name = message.GetString();

            return p;
        }
    }
}
