using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class TextMessagePacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, TextMessageType type, string text)
        {
            message.AddByte((byte)ServerPacketType.TextMessage);
            message.AddByte((byte)type);
            message.AddString(text);
        }
    }
}
