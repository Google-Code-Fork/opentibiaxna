using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using OpenTibiaXna.Helpers.Enums;
using OpenTibiaXna.OTServer.Helpers.ServerSettings;

namespace OpenTibiaXna.Helpers.ServerSettings
{
    /// <summary>
    /// Provide methods to manage the settings in config file
    /// </summary>
    public class SettingsManager
    {
        public static NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }

        /// <summary>
        /// Set/Create an applicaton entry in config file
        /// </summary>
        /// <param name="key">The key of the setting to be create/updated</param>
        /// <param name="value">The value of the setting</param>
        public static void SetAppSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        #region Get Methods

        public static string GetValueAsString(string appKey)
        {
            if (String.IsNullOrEmpty(appKey))
                throw new ArgumentNullException("appKey");

            string result = AppSettings.Get(appKey);

            return result;
        }

        public static int GetValueAsInt(string appKey)
        {
            int result = 0;
            
            int.TryParse(GetValueAsString(appKey), out result);

            return result;
        }

        /// <summary>
        /// Try to convert the app setting to boolean.
        /// </summary>
        /// <param name="appKey">The app key that have the boolean value</param>
        /// <param name="valueAsBool">When method returns the app key value is set in this out param</param>
        /// <returns>If the value can't be converted to boolean returns FALSE, otherwise returns TRUE.</returns>
        public static bool GetValueAsBool(string appKey, out bool valueAsBool)
        {
            return bool.TryParse(GetValueAsString(appKey), out valueAsBool);
        }

        /// <summary>
        /// Get the Login server port as set in config file
        /// </summary>
        /// <returns>If no port is specified, 7171 is returned as default.</returns>
        public static int GetLoginServerPort()
        {
            int loginServerPort = GetValueAsInt(Settings.LoginServerPort);

            return (loginServerPort == 0) ? loginServerPort : 7171;
        }

        /// <summary>
        /// Get the Game server port as set in config file
        /// </summary>
        /// <returns>If no port is specified, 7172 is returned as default.</returns>
        public static int GetGameServerPort()
        {
            int gameServerPort = GetValueAsInt(Settings.LoginServerPort);

            return (gameServerPort == 0) ? gameServerPort : 7172;
        }

        public static DatabaseTypes GetDatabaseType()
        {
            string databaseTypeAsString = GetValueAsString(Settings.DatabaseType);
            DatabaseTypes databaseType = DatabaseTypes.None;

            switch (databaseTypeAsString.ToLower())
            {
                case "0":
                case "mssql":
                    databaseType = DatabaseTypes.MSSQL;
                    break;
                case "1":
                case "mysql":
                    databaseType = DatabaseTypes.MySQL;
                    break;
                case "2":
                case "sqlite":
                    databaseType = DatabaseTypes.SQLite;
                    break;
                default:
                    databaseType = DatabaseTypes.None;
                    break;
            }

            return databaseType;
        }

        public static string GetGameWorldName()
        {
            return GetValueAsString(Settings.WorldName);
        }

        #endregion
    }
}