using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;

namespace OpenTibiaXna.OTServer.Items
{
    class Container : ItemObject
    {
        public byte Volume { get; set; }
        public List<ItemObject> Items { get; set; }

        public Container(ushort id)
            : base(id)
        {

        }

        public override string GetLookAtString()
        {
            return "You see " + Info.Article + " " + Info.Name +
                ". (Vol:" + Volume +
                Info.Description + Info.SpecialDescription +
                "\n It weighs " + (Info.Weight += Items.Sum(i => i.Info.Weight)) + " oz.";
        }
    }
}
