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

        /// <summary>
        /// Returns only GameWorld without relations entities, use that when you want to use only accounts information.
        /// </summary>
        /// <param name="lazyMode">If TRUE no relations entities will be returned. Otherwise returns the same of GetAll() method.</param>
        /// <returns></returns>
        public static List<GameWorld> GetAll(bool lazyMode)
        {
            if (lazyMode)
                return GenericDatabase.CurrentContext.CreateQuery<GameWorld>(typeof(GameWorld).Name).ToList();
            return GetAll();
        }

        public static GameWorld GetGameWorldBy(string gameWorldName)
        {
            GameWorld resultGameWorld = null;

            var queryResult = from gameWorlds in GetAll(true)
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
