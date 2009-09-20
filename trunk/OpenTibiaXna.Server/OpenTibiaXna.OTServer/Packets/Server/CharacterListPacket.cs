using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class CharacterListPacket : PacketObject
    {
        public static void Add(NetworkMessageEngine message, IEnumerable<CharacterListItem> characters, ushort premiumDaysLeft)
        {
            message.AddByte((byte)ServerPacketType.CharacterList);

            message.AddByte((byte)characters.Count());

            foreach (CharacterListItem character in characters)
            {
                message.AddString(character.Name);
                message.AddString(character.World);
                message.AddBytes(character.Ip);
                message.AddUInt16(character.Port);
            }

            message.AddUInt16(premiumDaysLeft);
        }

        public CharacterListPacket Parse(NetworkMessageEngine message)
        {
            return new CharacterListPacket();
        }
    }
}
