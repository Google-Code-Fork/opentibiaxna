using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Packets.Client
{
    public class VipRemovePacket
    {
        public uint Id { get; set; }

        public static VipRemovePacket Parse(NetworkMessage message)
        {
            VipRemovePacket p = new VipRemovePacket();
            p.Id = message.GetUInt32();
            return p;
        }
    }
}
