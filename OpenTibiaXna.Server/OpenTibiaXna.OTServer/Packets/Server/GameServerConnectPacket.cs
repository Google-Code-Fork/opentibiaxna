using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class GameServerConnectPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message)
        {
            message.AddByte(0x1F); // type

            message.AddUInt32(0x1337); // time in seconds since server start

            message.AddByte(0x10); // fractional time?
        }

        public GameServerConnectPacket Parse(NetworkMessageEngine message)
        {
            return new GameServerConnectPacket();
        }
    }
}
