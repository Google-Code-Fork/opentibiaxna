using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.Helpers.ServerSettings;
using OpenTibiaXna.Helpers.Enums;
using System.Data.Objects;

namespace OpenTibiaXna.OTServer.Engines
{
    public class DatabaseEngine
    {
        /// <summary>
        /// Set database type as in config file
        /// </summary>
        public static void SetDatabaseType()
        {
            SetDatabaseType(SettingsManager.GetDatabaseType());
        }

        public static void SetDatabaseType(DatabaseTypes databaseType)
        {
            switch (databaseType)
            {
                case DatabaseTypes.MSSQL:
                    GenericDatabase.CurrentContext = new MSSQLEntities();
                    break;
                case DatabaseTypes.SQLite:
                    GenericDatabase.CurrentContext = new SQLiteEntities();
                    break;
                case DatabaseTypes.MySQL:
                    GenericDatabase.CurrentContext = new MySQLEntities();
                    break;
                case DatabaseTypes.None:
                default:
                    throw new InvalidOperationException("Unknow DatabaseType in config file.");
            }
        }
    }
}
