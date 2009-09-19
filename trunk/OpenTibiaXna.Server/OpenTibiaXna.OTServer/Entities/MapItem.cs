using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Entities
{
    public partial class MapItem
    {
        public static List<MapItem> GetAll()
        {
            return GenericDatabase.CurrentContext.CreateQuery<MapItem>(typeof(MapItem).Name).ToList();
        }

        public static List<MapItem> GetAllOrderByStackPosition()
        {
            var queryResult = from results in GetAll()
                              orderby results.StackPosition
                              select results;

            return queryResult.ToList();
        }
    }
}
