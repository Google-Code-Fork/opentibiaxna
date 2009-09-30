using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;
using System.Net;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Engines
{
    public class AccountEngine
    {
        public static List<Account> GetAll()
        {
            return GenericDatabase.CurrentContext.CreateQuery<Account>(typeof(Account).Name).Include(typeof(Player).Name).ToList();
        }

        /// <summary>
        /// Returns only Accounts without relations entities, use that when you want to use only accounts information.
        /// </summary>
        /// <param name="lazyMode">If TRUE no relations entities will be returned. If FALSE returns the same of GetAll() method.</param>
        /// <returns></returns>
        public static List<Account> GetAll(bool lazyMode)
        {
            if (lazyMode)
                return GenericDatabase.CurrentContext.CreateQuery<Account>(typeof(Account).Name).ToList();
            return GetAll();
        }

        public static Account GetAccountBy(string accountName)
        {
            Account resultAccount = null;

            var queryResult = from accounts in GetAll()
                              where accounts.Name.Equals(accountName, StringComparison.InvariantCultureIgnoreCase)
                              select accounts;

            if (queryResult.Count() > 0)
                resultAccount = queryResult.First();

            return resultAccount;
        }

        public static Account GetAccountBy(string accountName, string accountPassword)
        {
            Account resultAccount = null;

            var queryResult = from accounts in GetAll()
                              where accounts.Name.Equals(accountName,
                                                        StringComparison.InvariantCultureIgnoreCase) &&
                              accounts.Password.Equals(accountPassword)
                              select accounts;

            if (queryResult.Count() > 0)
                resultAccount = queryResult.First();

            return resultAccount;
        }

        public static IEnumerable<CharacterListItem> GetCharacterList(Account account)
        {
            foreach (Player player in account.Player)
                yield return new CharacterListItem(
                    player.Name,
                    player.GameWorld.GameWorldName,
                    player.GameWorld.GameWorldIp,
                    player.GameWorld.GamePort);
        }
    }
}
