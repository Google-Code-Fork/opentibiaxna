﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Objects
{
    public partial class PlayerObject : CreatureObject
    {
        public ConnectionEngine Connection { get; set; }
        public Gender Gender { get; set; }
        public Vocation Vocation { get; set; }
        public ushort Level { get; set; }
        public byte MagicLevel { get; set; }
        public uint Experience { get; set; }
        public uint Capacity { get; set; }
        public LocationEngine SavedLocation { get; set; }
        public List<Channel> ChannelList { get; set; }
        public List<Channel> OpenedChannelList { get; set; }
        public FightMode FightMode { get; set; }
        public bool ChaseMode { get; set; }
        public bool SafeMode { get; set; }
        public Dictionary<uint, VipObject> VipList { get; set; }
        public int LastYellTime { get; set; }

        public PlayerObject()
        {
            Gender = Gender.Male;
            Vocation = Vocation.None;
            ChannelList = new List<Channel>();
            OpenedChannelList = new List<Channel>();
            ChannelList.Add(new Channel((ushort)ChatChannel.Game, "Game-Chat", 0));
            ChannelList.Add(new Channel((ushort)ChatChannel.RealLife, "RL-Chat", 0));
            ChannelList.Add(new Channel((ushort)ChatChannel.Help, "Help", 0));
            VipList = new Dictionary<uint, VipObject>(100);
        }
    }
}
