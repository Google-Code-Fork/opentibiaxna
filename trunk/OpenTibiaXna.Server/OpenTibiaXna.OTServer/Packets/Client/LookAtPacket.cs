using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Client
{
    public class LookAtPacket : PacketObject
    {
        public LocationEngine Location { get; private set; }
        public ushort Id { get; private set; }
        public byte StackPosition { get; private set; }

        public static LookAtPacket Parse(NetworkMessageEngine message)
        {
            LookAtPacket packet = new LookAtPacket();

            packet.Location = message.GetLocation();
            packet.Id = message.GetUInt16();
            packet.StackPosition = message.GetByte();

            return packet;
        }
    }
}
