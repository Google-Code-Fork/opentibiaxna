using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return "You see " + Article + " " + Name +
                ". (Vol:" + Volume +
                Description + SpecialDescription +
                "\n It weighs " + (Weight += Items.Sum(P => P.Weight)) + " oz.";
        }
    }
}
