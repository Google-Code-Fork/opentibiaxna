using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.OTServer.Objects;
using System.Data.Objects.DataClasses;

namespace OpenTibiaXna.OTServer.Engines
{
    public class PlayerEngine
    {
        public static List<Player> GetAll()
        {
            return GenericDatabase.CurrentContext.CreateQuery<Player>(typeof(Player).Name).Include(typeof(GameWorld).Name).Include(typeof(Account).Name).ToList();
        }

        public static Player GetPlayerBy(int accountId, string characterName)
        {
            Player resultPlayer = null;

            var queryResult = from players in GetAll()
                              where players.Account.AccountId.Equals(accountId) &&
                              players.Name.Equals(characterName)
                              select players;

            if (queryResult.Count() > 0)
                resultPlayer = queryResult.First();

            return resultPlayer;
        }

        internal static Player GetPlayerBy(uint playerId)
        {
            return GetAll().Where(player => player.PlayerId.Equals(playerId)).First();
        }
    }
}
