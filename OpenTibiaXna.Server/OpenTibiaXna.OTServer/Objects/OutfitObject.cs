using System;
using System.Collections.Generic;

namespace OpenTibiaXna.OTServer
{
    public class OutfitObject
    {
        public UInt16 LookType;
        public UInt16 LookItem;
        public byte Head;
        public byte Body;
        public byte Legs;
        public byte Feet;
        public byte Addons;
        public string Name = "";

        public OutfitObject(UInt16 lookType, byte head, byte body, byte legs, byte feet, byte addons)
        {
            LookType = lookType;
            Head = head;
            Body = body;
            Legs = legs;
            Feet = feet;
            Addons = addons;
            LookItem = 0;
        }

        public OutfitObject(UInt16 lookType, UInt16 lookItem)
        {
            LookType = lookType;
            LookItem = lookItem;
            Head = 0;
            Body = 0;
            Legs = 0;
            Feet = 0;
            Addons = 0;
        }

        public OutfitObject(string name, UInt16 lookType, byte addons)
        {
            Name = name;
            LookType = lookType;
            Addons = addons;
        }
    }
}