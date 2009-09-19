using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace OpenTibiaXna.OTServer.Entities
{
    public partial class Account
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
    }
}
