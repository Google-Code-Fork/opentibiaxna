﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Net;
using System.Security.Cryptography;
using OpenTibiaXna.OTServer.Entities;
using System.Data.Objects;
using OpenTibiaXna.Helpers;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Items;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer
{
    public class Database
    {
        //Player IDs and names are static and stored in the database
        //p.s.: Names with different casing are considered equal and
        // shall not be allowed.
        //e.g.: rIcHarD==richard==Richard
        private static SQLiteConnection connection = new SQLiteConnection(
            "OpenTibiaXna.OTServer.Properties.Settings.Default.ConnectionString"
        );

        #region Account Commands

        private static SQLiteCommand insertAccountCommand = new SQLiteCommand(
            @"insert into Account (Name, Password)
              values (@accountName, @password)",
            connection
        );

        private static SQLiteCommand deleteAccountCommand = new SQLiteCommand(
            @"delete from Account
                where Id=@accountId",
            connection
        );

        private static SQLiteCommand selectAccountIdCommand = new SQLiteCommand(
            @"select Id
              from Account
              where Name = @accountName
              and Password = @password",
            connection
        );

        private static SQLiteCommand selectAllAccountNamesCommand = new SQLiteCommand(
            @"select Name
              from Account",
            connection
        );

        private static SQLiteCommand checkAccountNameCommand = new SQLiteCommand(
            @"select Name
              from Account
              where Name=@accountName",
            connection
        );

        private static SQLiteCommand checkPlayerNameCommand = new SQLiteCommand(
            @"select Name
              from Player
              where Name=@name",
            connection
        );

        private static SQLiteCommand checkPlayerIdCommand = new SQLiteCommand(
            @"select Id
              from Player
              where Id=@playerId",
            connection
        );

        private static SQLiteCommand selectLastInsertId = new SQLiteCommand(
            "select last_insert_rowid()",
            connection
        );

        private static SQLiteCommand updatePasswordByAccountId = new SQLiteCommand(
            @"update Account
                set
                    Password=@password
                where Id=@accountId",
            connection
        );

        #endregion

        #region Map Commands
        private static SQLiteCommand selectMapTilesCommand = new SQLiteCommand(
            @"select * from MapTile",
            connection
        );

        private static SQLiteCommand selectMapItemsCommand = new SQLiteCommand(
            @"select * from MapItem order by StackPosition",
            connection
        );
        #endregion

        #region Player Commands

        private static SQLiteCommand insertPlayerCommand = new SQLiteCommand(
            @"insert into Player 
                (AccountId, Id, Name, Gender, Vocation, Level, MagicLevel, 
                 Experience, MaxHealth,  MaxMana, Capacity, OutfitLookType, 
                 OutfitHead, OutfitBody, OutfitLegs, OutfitFeet, OutfitAddons)
              values
                (@accountId, @playerId, @name, @gender, @vocation, @level, @magicLevel,
                 @experience, @maxHealth, @maxMana, @capacity, @outfitLookType,
                 @outfitHead, @outfitBody, @outfitLegs, @outfitFeet, @outfitAddons)",
            connection
        );

        private static SQLiteCommand deletePlayerByName = new SQLiteCommand(
            @"delete from Player
                where Name=@name",
            connection
        );

        private static SQLiteCommand selectPlayerNameByAccountIdCommand = new SQLiteCommand(
            @"select Name
              from Player
              where AccountId = @accountId",
            connection
        );

        private static SQLiteCommand selectPlayerByNameCommand = new SQLiteCommand(
            @"select
                Id, Gender, Vocation, Level, MagicLevel,Experience, MaxHealth, 
                MaxMana, Capacity, OutfitLookType, OutfitHead, OutfitBody, 
                OutfitLegs, OutfitFeet, OutfitAddons, LocationX, LocationY, 
                LocationZ, Direction
              from Player
              where Name = @name",
            connection
        );

        private static SQLiteCommand selectPlayerByIdCommand = new SQLiteCommand(
            @"select
                Name, Gender, Vocation, Level, MagicLevel,Experience, MaxHealth, 
                MaxMana, Capacity, OutfitLookType, OutfitHead, OutfitBody, 
                OutfitLegs, OutfitFeet, OutfitAddons, LocationX, LocationY, 
                LocationZ, Direction
              from Player
              where Id = @playerId",
            connection
        );

        private static SQLiteCommand selectAllPlayersCommand = new SQLiteCommand(
            @"select
                Id, Name, Gender, Vocation, Level, MagicLevel,Experience, MaxHealth, 
                MaxMana, Capacity, OutfitLookType, OutfitHead, OutfitBody, 
                OutfitLegs, OutfitFeet, OutfitAddons, LocationX, LocationY, 
                LocationZ, Direction
              from Player",
            connection
        );

        private static SQLiteCommand selectPlayerIdNamePairsCommand = new SQLiteCommand(
            @"select
                Id, Name
            from Player",
            connection
        );

        private static SQLiteCommand selectPlayerNameByIdCommand = new SQLiteCommand(
            @"select
                Name
            from Player
            where Id=@playerId",
            connection
        );

        private static SQLiteCommand selectPlayerIdByNameCommand = new SQLiteCommand(
            @"select
                Id
            from Player
            where Name=@name",
            connection
        );

        private static SQLiteCommand updatePlayerByNameCommand = new SQLiteCommand(
            @"update Player
              set
                  Id = @playerId,
                  Gender = @gender,
                  Vocation = @vocation,
                  Level = @level,
                  MagicLevel = @magicLevel,
                  Experience = @experience,
                  MaxHealth = @maxHealth,
                  MaxMana = @maxMana,
                  Capacity = @capacity,
                  OutfitLookType = @outfitLookType,
                  OutfitHead = @outfitHead,
                  OutfitBody = @outfitBody,
                  OutfitLegs = @outfitLegs,
                  OutfitFeet = @outfitFeet,
                  OutfitAddons = @outfitAddons,
                  LocationX = @locationX,
                  LocationY = @locationY,
                  LocationZ = @locationZ,
                  Direction = @direction
              where Name = @name",
            connection
        );

        private static SQLiteCommand updatePlayerByIdCommand = new SQLiteCommand(
            @"update Player
              set
                  Name = @name,
                  Gender = @gender,
                  Vocation = @vocation,
                  Level = @level,
                  MagicLevel = @magicLevel,
                  Experience = @experience,
                  MaxHealth = @maxHealth,
                  MaxMana = @maxMana,
                  Capacity = @capacity,
                  OutfitLookType = @outfitLookType,
                  OutfitHead = @outfitHead,
                  OutfitBody = @outfitBody,
                  OutfitLegs = @outfitLegs,
                  OutfitFeet = @outfitFeet,
                  OutfitAddons = @outfitAddons,
                  LocationX = @locationX,
                  LocationY = @locationY,
                  LocationZ = @locationZ,
                  Direction = @direction
              where Id = @playerId",
            connection
        );

        #endregion

        #region Parameters

        private static SQLiteParameter accountNameParam = new SQLiteParameter("accountName");
        private static SQLiteParameter passwordParam = new SQLiteParameter("password");
        private static SQLiteParameter accountIdParam = new SQLiteParameter("accountId");
        private static SQLiteParameter playerNameParam = new SQLiteParameter("name");

        private static SQLiteParameter playerIdParam = new SQLiteParameter("playerId");
        private static SQLiteParameter genderParam = new SQLiteParameter("gender");
        private static SQLiteParameter vocationParam = new SQLiteParameter("vocation");
        private static SQLiteParameter levelParam = new SQLiteParameter("level");
        private static SQLiteParameter magicLevelParam = new SQLiteParameter("magicLevel");
        private static SQLiteParameter experienceParam = new SQLiteParameter("experience");
        private static SQLiteParameter maxHealthParam = new SQLiteParameter("maxHealth");
        private static SQLiteParameter maxManaParam = new SQLiteParameter("maxMana");
        private static SQLiteParameter capacityParam = new SQLiteParameter("capacity");
        private static SQLiteParameter outfitLookTypeParam = new SQLiteParameter("outfitLookType");
        private static SQLiteParameter outfitHeadParam = new SQLiteParameter("outfitHead");
        private static SQLiteParameter outfitBodyParam = new SQLiteParameter("outfitBody");
        private static SQLiteParameter outfitLegsParam = new SQLiteParameter("outfitLegs");
        private static SQLiteParameter outfitFeetParam = new SQLiteParameter("outfitFeet");
        private static SQLiteParameter outfitAddonsParam = new SQLiteParameter("outfitAddons");
        private static SQLiteParameter locationXParam = new SQLiteParameter("locationX");
        private static SQLiteParameter locationYParam = new SQLiteParameter("locationY");
        private static SQLiteParameter locationZParam = new SQLiteParameter("locationZ");
        private static SQLiteParameter directionParam = new SQLiteParameter("direction");

        #endregion

        #region Setup

        static Database()
        {
            //connection.Open();

            ////Account management parameters
            //insertAccountCommand.Parameters.Add(accountNameParam);
            //insertAccountCommand.Parameters.Add(passwordParam);

            //deleteAccountCommand.Parameters.Add(accountIdParam);

            //selectAccountIdCommand.Parameters.Add(accountNameParam);
            //selectAccountIdCommand.Parameters.Add(passwordParam);

            //selectPlayerNameByAccountIdCommand.Parameters.Add(accountIdParam);

            //checkAccountNameCommand.Parameters.Add(accountNameParam);

            //checkPlayerNameCommand.Parameters.Add(playerNameParam);

            //checkPlayerIdCommand.Parameters.Add(playerIdParam);

            //updatePasswordByAccountId.Parameters.Add(accountIdParam);
            //updatePasswordByAccountId.Parameters.Add(passwordParam);
            ////Players parameters
            //insertPlayerCommand.Parameters.Add(accountIdParam);
            //insertPlayerCommand.Parameters.Add(playerIdParam);
            //insertPlayerCommand.Parameters.Add(playerNameParam);
            //insertPlayerCommand.Parameters.Add(genderParam);
            //insertPlayerCommand.Parameters.Add(vocationParam);
            //insertPlayerCommand.Parameters.Add(levelParam);
            //insertPlayerCommand.Parameters.Add(magicLevelParam);
            //insertPlayerCommand.Parameters.Add(experienceParam);
            //insertPlayerCommand.Parameters.Add(maxHealthParam);
            //insertPlayerCommand.Parameters.Add(maxManaParam);
            //insertPlayerCommand.Parameters.Add(capacityParam);
            //insertPlayerCommand.Parameters.Add(outfitLookTypeParam);
            //insertPlayerCommand.Parameters.Add(outfitHeadParam);
            //insertPlayerCommand.Parameters.Add(outfitBodyParam);
            //insertPlayerCommand.Parameters.Add(outfitLegsParam);
            //insertPlayerCommand.Parameters.Add(outfitFeetParam);
            //insertPlayerCommand.Parameters.Add(outfitAddonsParam);

            //deletePlayerByName.Parameters.Add(playerNameParam);

            //selectPlayerByNameCommand.Parameters.Add(playerNameParam);

            //selectPlayerIdByNameCommand.Parameters.Add(playerNameParam);

            //selectPlayerByIdCommand.Parameters.Add(playerIdParam);

            //selectPlayerNameByIdCommand.Parameters.Add(playerIdParam);

            //updatePlayerByNameCommand.Parameters.Add(playerIdParam);
            //AddUpdateParams(updatePlayerByNameCommand);
            //updatePlayerByNameCommand.Parameters.Add(playerNameParam);

            //updatePlayerByIdCommand.Parameters.Add(playerNameParam);
            //AddUpdateParams(updatePlayerByIdCommand);
            //updatePlayerByIdCommand.Parameters.Add(playerIdParam);
        }

        public static void AddUpdateParams(SQLiteCommand command)
        {
            command.Parameters.Add(genderParam);
            command.Parameters.Add(vocationParam);
            command.Parameters.Add(levelParam);
            command.Parameters.Add(magicLevelParam);
            command.Parameters.Add(experienceParam);
            command.Parameters.Add(maxHealthParam);
            command.Parameters.Add(maxManaParam);
            command.Parameters.Add(capacityParam);
            command.Parameters.Add(outfitLookTypeParam);
            command.Parameters.Add(outfitHeadParam);
            command.Parameters.Add(outfitBodyParam);
            command.Parameters.Add(outfitLegsParam);
            command.Parameters.Add(outfitFeetParam);
            command.Parameters.Add(outfitAddonsParam);
            command.Parameters.Add(locationXParam);
            command.Parameters.Add(locationYParam);
            command.Parameters.Add(locationZParam);
            command.Parameters.Add(directionParam);

        }

        public static void Close()
        {
            connection.Close();
        }

        #endregion

        #region Account Management

        public static int GetLastInsertId()
        {
            return Convert.ToInt32(selectLastInsertId.ExecuteScalar());
        }

        public static int CreateAccount(string name, string password)
        {
            int id = -1;

            accountNameParam.Value = name;
            passwordParam.Value = Hash.SHA256Hash(password);

            try
            {
                if (1 == insertAccountCommand.ExecuteNonQuery())
                {
                    id = GetLastInsertId();
                }
            }
            catch (SQLiteException)
            {
                return id;
            }

            return id;
        }

        public static bool DeleteAccount(long accountId)
        {
            accountIdParam.Value = accountId;

            return (1 == deleteAccountCommand.ExecuteNonQuery());
        }

        public static long GetAccountId(string accountName, string password)
        {
            return 0x00;
        }

        public static int CreatePlayer(long accountId, string name, uint playerid)
        {
            int id = -1;

            accountIdParam.Value = accountId;
            playerIdParam.Value = playerid;
            playerNameParam.Value = name;
            genderParam.Value = Gender.Male;
            vocationParam.Value = Vocation.None;
            levelParam.Value = 1;
            magicLevelParam.Value = 0;
            experienceParam.Value = 0;
            maxHealthParam.Value = 100;
            maxManaParam.Value = 0;
            capacityParam.Value = 100;
            outfitLookTypeParam.Value = 128;
            outfitHeadParam.Value = 0;
            outfitBodyParam.Value = 0;
            outfitLegsParam.Value = 0;
            outfitFeetParam.Value = 0;
            outfitAddonsParam.Value = 0;

            try
            {
                if (1 == insertPlayerCommand.ExecuteNonQuery())
                {
                    id = GetLastInsertId();
                }
            }
            catch (SQLiteException)
            {
                return id;
            }

            return id;
        }

        public static bool DeletePlayerByName(string playerName)
        {
            playerNameParam.Value = playerName;
            return (1 == deletePlayerByName.ExecuteNonQuery());
        }

        public static IEnumerable<string> GetAllAccountNames()
        {
            SQLiteDataReader reader = selectAllAccountNamesCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    yield return reader.GetString(0);
                }
            }
            finally
            {
                reader.Close();
            }
        }

        public static IEnumerable<CharacterListItem> GetCharacterList(Account account)
        {
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var ipBytes = ipAddress.GetAddressBytes();


            foreach (Player player in account.Player)
            {
                yield return new CharacterListItem(
                    player.Name,
                    "OpenTibiaXna.OTServer.Properties.Settings.Default.WorldName",
                    ipBytes,
                    7171);
            }
        }

        public static bool AccountNameExists(string accName)
        {
            accountNameParam.Value = accName;
            return null != checkAccountNameCommand.ExecuteScalar();
        }

        public static bool PlayerNameExists(string playerName)
        {
            playerNameParam.Value = playerName;
            return null != checkPlayerNameCommand.ExecuteScalar();
        }

        public static bool PlayerIdExists(uint id)
        {
            playerIdParam.Value = id;
            return null != checkPlayerIdCommand.ExecuteScalar();
        }

        public static bool UpdateAccountPassword(long accountId, string newPassword)
        {
            accountIdParam.Value = accountId;
            passwordParam.Value = Hash.SHA256Hash(newPassword);

            return (1 == updatePasswordByAccountId.ExecuteNonQuery());
        }

        #endregion

        #region Players
        public static PlayerObject GetPlayerByName(long accountId, string name)
        {
            PlayerObject player = null;
            accountIdParam.Value = accountId;
            playerNameParam.Value = name;

            SQLiteDataReader reader = selectPlayerByNameCommand.ExecuteReader();
            if (reader.Read())
            {
                player = new PlayerObject();
                player.Name = name;
                player.Id = (uint)reader.GetInt32(0);
                player.Gender = (Gender)reader.GetByte(1);
                player.Vocation = (Vocation)reader.GetByte(2);
                player.Level = (ushort)reader.GetInt16(3);
                player.MagicLevel = reader.GetByte(4);
                player.Experience = (uint)reader.GetInt32(5);
                player.MaxHealth = (ushort)reader.GetInt16(6);
                player.MaxMana = (ushort)reader.GetInt16(7);
                player.Capacity = (uint)reader.GetInt32(8);
                player.Outfit.LookType = (ushort)reader.GetInt16(9);
                player.Outfit.Head = reader.GetByte(10);
                player.Outfit.Body = reader.GetByte(11);
                player.Outfit.Legs = reader.GetByte(12);
                player.Outfit.Feet = reader.GetByte(13);
                player.Outfit.Addons = reader.GetByte(14);

                if (!reader.IsDBNull(15))
                {
                    int x = reader.GetInt32(15);
                    int y = reader.GetInt32(16);
                    int z = reader.GetInt32(17);
                    player.SavedLocation = new LocationEngine(x, y, z);
                    player.Direction = (Direction)reader.GetByte(18);
                }

                player.Speed = (ushort)(220 + (2 * (player.Level - 1)));
            }
            reader.Close();
            return player;
        }

        public static PlayerObject GetPlayerById(uint playerId)
        {
            PlayerObject player = null;
            playerIdParam.Value = playerId;

            SQLiteDataReader reader = selectPlayerByIdCommand.ExecuteReader();
            if (reader.Read())
            {
                player = new PlayerObject();
                player.Name = reader.GetString(0);
                player.Id = playerId;
                player.Gender = (Gender)reader.GetByte(1);
                player.Vocation = (Vocation)reader.GetByte(2);
                player.Level = (ushort)reader.GetInt16(3);
                player.MagicLevel = reader.GetByte(4);
                player.Experience = (uint)reader.GetInt32(5);
                player.MaxHealth = (ushort)reader.GetInt16(6);
                player.MaxMana = (ushort)reader.GetInt16(7);
                player.Capacity = (uint)reader.GetInt32(8);
                player.Outfit.LookType = (ushort)reader.GetInt16(9);
                player.Outfit.Head = reader.GetByte(10);
                player.Outfit.Body = reader.GetByte(11);
                player.Outfit.Legs = reader.GetByte(12);
                player.Outfit.Feet = reader.GetByte(13);
                player.Outfit.Addons = reader.GetByte(14);

                if (!reader.IsDBNull(15))
                {
                    int x = reader.GetInt32(15);
                    int y = reader.GetInt32(16);
                    int z = reader.GetInt32(17);
                    player.SavedLocation = new LocationEngine(x, y, z);
                    player.Direction = (Direction)reader.GetByte(18);
                }

                player.Speed = (ushort)(220 + (2 * (player.Level - 1)));
            }
            reader.Close();
            return player;
        }

        public static bool SavePlayerByName(PlayerObject player)
        {
            PlayerInfoToParams(player);

            return (1 == updatePlayerByNameCommand.ExecuteNonQuery());
        }

        public static bool SavePlayerById(PlayerObject player)
        {
            PlayerInfoToParams(player);

            return (1 == updatePlayerByIdCommand.ExecuteNonQuery());
        }

        private static void PlayerInfoToParams(PlayerObject player)
        {
            accountIdParam.Value = player.Connection.AccountId;
            playerIdParam.Value = player.Id;
            playerNameParam.Value = player.Name;
            genderParam.Value = player.Gender;
            vocationParam.Value = player.Vocation;
            levelParam.Value = player.Level;
            magicLevelParam.Value = player.MagicLevel;
            experienceParam.Value = player.Experience;
            maxHealthParam.Value = player.MaxHealth;
            maxManaParam.Value = player.MaxMana;
            capacityParam.Value = player.Capacity;
            outfitLookTypeParam.Value = player.Outfit.LookType;
            outfitHeadParam.Value = player.Outfit.Head;
            outfitBodyParam.Value = player.Outfit.Body;
            outfitLegsParam.Value = player.Outfit.Legs;
            outfitFeetParam.Value = player.Outfit.Feet;
            outfitAddonsParam.Value = player.Outfit.Addons;
            locationXParam.Value = player.Tile.Location.X;
            locationYParam.Value = player.Tile.Location.Y;
            locationZParam.Value = player.Tile.Location.Z;
            directionParam.Value = player.Direction;
        }

        public static IEnumerable<PlayerObject> GetAllPlayers()
        {
            SQLiteDataReader reader = selectAllPlayersCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    PlayerObject player = new PlayerObject();
                    player.Id = (uint)reader.GetInt32(0);
                    player.Name = reader.GetString(1);
                    player.Gender = (Gender)reader.GetByte(2);
                    player.Vocation = (Vocation)reader.GetByte(3);
                    player.Level = (ushort)reader.GetInt16(4);
                    player.MagicLevel = reader.GetByte(5);
                    player.Experience = (uint)reader.GetInt32(6);
                    player.MaxHealth = (ushort)reader.GetInt16(7);
                    player.MaxMana = (ushort)reader.GetInt16(8);
                    player.Capacity = (uint)reader.GetInt32(9);
                    player.Outfit.LookType = (ushort)reader.GetInt16(10);
                    player.Outfit.Head = reader.GetByte(11);
                    player.Outfit.Body = reader.GetByte(12);
                    player.Outfit.Legs = reader.GetByte(13);
                    player.Outfit.Feet = reader.GetByte(14);
                    player.Outfit.Addons = reader.GetByte(15);
                    if (!reader.IsDBNull(16))
                    {
                        int x = reader.GetInt32(16);
                        int y = reader.GetInt32(17);
                        int z = reader.GetInt32(18);
                        player.SavedLocation = new LocationEngine(x, y, z);
                        player.Direction = (Direction)reader.GetByte(19);
                    }
                    yield return player;
                }
            }
            finally
            {
                reader.Close();
            }
        }

        public static Dictionary<uint, string> GetPlayerIdNameDictionary()
        {
            Dictionary<uint, string> dictionary = new Dictionary<uint, string>();

            SQLiteDataReader reader = selectPlayerIdNamePairsCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    dictionary.Add((uint)reader.GetInt32(0), reader.GetString(1));
                }
            }
            finally
            {
                reader.Close();
            }
            return dictionary;
        }

        public static KeyValuePair<uint, string> GetPlayerIdNamePair(uint playerId)
        {
            KeyValuePair<uint, string> pair = new KeyValuePair<uint, string>();
            playerIdParam.Value = playerId;
            SQLiteDataReader reader = selectPlayerNameByIdCommand.ExecuteReader();
            if (reader.Read())
                pair = new KeyValuePair<uint, string>(playerId, reader.GetString(0));
            reader.Close();
            return pair;
        }

        public static KeyValuePair<uint, string> GetPlayerIdNamePair(string playerName)
        {
            KeyValuePair<uint, string> pair = new KeyValuePair<uint, string>();
            playerNameParam.Value = playerName;
            SQLiteDataReader reader = selectPlayerIdByNameCommand.ExecuteReader();
            if (reader.Read())
                pair = new KeyValuePair<uint, string>((uint)reader.GetInt32(0), playerName);
            reader.Close();
            return pair;
        }

        #endregion

        #region Map

        public static void GetMapTiles(MapObject map)
        {
            foreach (MapTile mapTile in MapTile.GetAll())
            {
                TileObject tile = new TileObject();
                int x = mapTile.X - 32000;
                int y = mapTile.Y - 32000;
                int z = mapTile.Z;
                tile.Ground = new ItemObject((ushort)mapTile.GroundId);
                LocationEngine location = new LocationEngine(x, y, z);
                map.SetTile(location, tile);
            }
        }

        public static void GetMapItems(MapObject map)
        {
            foreach (MapItem mapItem in MapItem.GetAll().OrderBy(o => o.StackPosition))
            {
                int x = mapItem.X - 32000;
                int y = mapItem.Y - 32000;
                int z = mapItem.Z;
                ushort id = (ushort)mapItem.Id;
                byte extra = (byte)mapItem.Extra;

                TileObject tile = map.GetTile(x, y, z);
                if (tile != null)
                {
                    ItemObject item = new ItemObject(id);
                    item.Extra = extra;
                    tile.Items.Add(item);
                }
            }
        }

        #endregion
    }
}
