﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Packets.Server
{
    public class PlayerStatusPacket : PacketObject
    {
        public static void Add
        (
            NetworkMessageEngine message,
            ushort health,
            ushort maxHealth,
            uint capacity,
            uint experience,
            ushort level,
            byte levelPercent,
            ushort mana,
            ushort maxMana,
            byte magicLevel,
            byte magicLevelPercent,
            byte soul,
            ushort stamina
        )
        {
            message.AddByte((byte)ServerPacketType.PlayerStatus);

            message.AddUInt16(health);
            message.AddUInt16(maxHealth);
            message.AddUInt32(capacity);

            message.AddUInt32(experience);

            message.AddUInt16(level);

            message.AddByte(levelPercent);

            message.AddUInt16(mana);
            message.AddUInt16(maxMana);

            message.AddByte(magicLevel);
            message.AddByte(magicLevelPercent);
            message.AddByte(soul);

            message.AddUInt16(stamina);
        }
    }
}
