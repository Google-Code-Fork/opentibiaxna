using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class MessageOfTheDayPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, string messageOfTheDay)
        {
            message.AddByte((byte)ServerPacketType.MessageOfTheDay);
            message.AddString("1\n" + messageOfTheDay);
        }

        public MessageOfTheDayPacket Parse(NetworkMessageEngine message)
        {
            return new MessageOfTheDayPacket();
        }
    }
}
