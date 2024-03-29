﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.OpenTibia
{
    public class Constants
    {
        public const byte NodeStart = 0xFE;
        public const byte NodeEnd = 0xFF;
        public const byte Escape = 0xFD;
    }

    public enum OtbmNodeType
    {
        Root = 1,
        MapData = 2,
        ItemDef = 3,
        TileArea = 4,
        Tile = 5,
        Item = 6,
        TileSquare = 7,
        TileRef = 8,
        Spawns = 9,
        SpawnArea = 10,
        Monster = 11,
        Towns = 12,
        Town = 13,
        HouseTile = 14,
        WayPoints = 15,
        WayPoint = 16
    }

    enum OtbmAttribute : byte
    {
        Description = 1,
        ExtFile = 2,
        TileFlags = 3,
        ActionId = 4,
        UniqueId = 5,
        Text = 6,
        Desc = 7,
        TeleportDestination = 8,
        Item = 9,
        DepotId = 10,
        ExtSpawnFile = 11,
        RuneCharges = 12,
        ExtHouseFile = 13,
        HouseDoorId = 14,
        Count = 15,
        Duration = 16,
        DecayingState = 17,
        WrittenDate = 18,
        WrittenBy = 19,
        SleeperId = 20,
        SleepStart = 21,
        Charges = 22
    }

    enum TileFlags
    {
        None = 0,
        ProtectionZone = 1 << 0,
        DeprecatedHouse = 1 << 1,
        NoPvpZone = 1 << 2,
        NoLogout = 1 << 3,
        PvpZone = 1 << 4,
        Refresh = 1 << 5,

        //internal usage
        House = 1 << 6,
        FloorChange = 1 << 7,
        FloorChangeDown = 1 << 8,
        FloorChangeNorth = 1 << 9,
        FloorChangeSouth = 1 << 10,
        FloorChangeEast = 1 << 11,
        FloorChangeWest = 1 << 12,
        Teleport = 1 << 13,
        MagicField = 1 << 14,
        Mailbox = 1 << 15,
        TrashHolder = 1 << 16,
        Bed = 1 << 17,
        BlocksSolid = 1 << 18,
        BlocksPath = 1 << 19,
        ImmoveableBlocksSolid = 1 << 20,
        ImmoveableBlocksPath = 1 << 21,
        ImmoveableNoFieldBlocksPath = 1 << 22,
        NoFieldBlocksPath = 1 << 23,
        Dynamic = 1 << 24
    }

    public enum ItemAttribute : byte
    {
        ServerId = 0x10,
        ClientId,
        ITEM_ATTR_NAME,				/*deprecated*/
        ITEM_ATTR_DESCR,			/*deprecated*/
        Speed,
        ITEM_ATTR_SLOT,				/*deprecated*/
        ITEM_ATTR_MAXITEMS,			/*deprecated*/
        ITEM_ATTR_WEIGHT,			/*deprecated*/
        ITEM_ATTR_WEAPON,			/*deprecated*/
        ITEM_ATTR_AMU,				/*deprecated*/
        ITEM_ATTR_ARMOR,			/*deprecated*/
        ITEM_ATTR_MAGLEVEL,			/*deprecated*/
        ITEM_ATTR_MAGFIELDTYPE,		/*deprecated*/
        ITEM_ATTR_WRITEABLE,		/*deprecated*/
        ITEM_ATTR_ROTATETO,			/*deprecated*/
        ITEM_ATTR_DECAY,			/*deprecated*/
        ITEM_ATTR_SPRITEHASH,
        ITEM_ATTR_MINIMAPCOLOR,
        ITEM_ATTR_07,
        ITEM_ATTR_08,
        ITEM_ATTR_LIGHT,

        //1-byte aligned
        ITEM_ATTR_DECAY2,			/*deprecated*/
        ITEM_ATTR_WEAPON2,			/*deprecated*/
        ITEM_ATTR_AMU2,				/*deprecated*/
        ITEM_ATTR_ARMOR2,			/*deprecated*/
        ITEM_ATTR_WRITEABLE2,		/*deprecated*/
        ITEM_ATTR_LIGHT2,
        TopOrder,
        ITEM_ATTR_WRITEABLE3		/*deprecated*/
    }
}
