using System;
using System.Collections.Generic;

namespace OpenTibiaXna.OTServer.Objects
{
    public class CharacterListItem
    {
        public string Name { get; set; }
        public string World { get; set; }
        public byte[] Ip { get; set; }
        public ushort Port { get; set; }

        public CharacterListItem(string name, string world, byte[] ip, ushort port)
        {
            Name = name;
            World = world;
            Ip = ip;
            Port = port;
        }
    }
}