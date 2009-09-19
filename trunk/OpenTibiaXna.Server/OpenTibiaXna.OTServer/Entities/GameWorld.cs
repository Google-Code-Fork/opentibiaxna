using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Entities
{
    public partial class GameWorld
    {
        public static List<GameWorld> GetAll()
        {
            return GenericDatabase.CurrentContext.CreateQuery<GameWorld>(typeof(GameWorld).Name).Include(typeof(Player).Name).ToList();
        }

        public static GameWorld GetGameWorldBy(string gameWorldName)
        {
            GameWorld resultGameWorld = null;

            var queryResult = from gameWorlds in GetAll()
                              where gameWorlds.GameWorldName.Equals(gameWorldName, StringComparison.InvariantCultureIgnoreCase)
                              select gameWorlds;

            if (queryResult.Count() > 0)
                resultGameWorld = queryResult.First();

            return resultGameWorld;
        }

        // Fix for databases that dont suport unsigned types
        public ushort GamePort 
        { 
            get { return (ushort)this.GameWorldPort; }
            set { this.GameWorldPort = Convert.ToInt16(value); }
        }
    }
}
