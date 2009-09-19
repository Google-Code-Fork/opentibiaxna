using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class VipLogoutPacket:Packet
    {
        public static void Add(NetworkMessage message,uint id)
        {
            message.AddByte((byte)ServerPacketType.VipLogout);
            message.AddUInt32(id);
        }
    }
}
