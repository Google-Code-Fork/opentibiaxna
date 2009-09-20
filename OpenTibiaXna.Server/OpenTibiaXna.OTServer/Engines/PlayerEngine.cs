using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.OTServer.Objects;

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
            {
                resultPlayer = queryResult.First();
                resultPlayer.PlayerObject = new PlayerObject();
                resultPlayer.PlayerObject.Name = resultPlayer.Name;
                resultPlayer.PlayerObject.Id = (uint)resultPlayer.PlayerId;
                resultPlayer.PlayerObject.Gender = (Gender)resultPlayer.Gender;
                resultPlayer.PlayerObject.Vocation = (Vocation)resultPlayer.Vocation;
                resultPlayer.PlayerObject.Level = (ushort)resultPlayer.Level;
                resultPlayer.PlayerObject.MagicLevel = Convert.ToByte(resultPlayer.MagicLevel);
                resultPlayer.PlayerObject.Experience = (uint)resultPlayer.Experience;
                resultPlayer.PlayerObject.MaxHealth = (ushort)resultPlayer.MaxHealth;
                resultPlayer.PlayerObject.MaxMana = (ushort)resultPlayer.MaxMana;
                resultPlayer.PlayerObject.Capacity = (uint)resultPlayer.Capacity;
                resultPlayer.PlayerObject.Outfit.LookType = (ushort)resultPlayer.OutfitLookType;
                resultPlayer.PlayerObject.Outfit.Head = Convert.ToByte(resultPlayer.OutfitHead);
                resultPlayer.PlayerObject.Outfit.Body = Convert.ToByte(resultPlayer.OutfitBody);
                resultPlayer.PlayerObject.Outfit.Legs = Convert.ToByte(resultPlayer.OutfitLegs);
                resultPlayer.PlayerObject.Outfit.Feet = Convert.ToByte(resultPlayer.OutfitFeet);
                resultPlayer.PlayerObject.Outfit.Addons = Convert.ToByte(resultPlayer.OutfitAddons);

                if (resultPlayer.LocationX.HasValue)
                {
                    int x = resultPlayer.LocationX.Value;
                    int y = resultPlayer.LocationY.Value;
                    int z = resultPlayer.LocationZ.Value;
                    resultPlayer.PlayerObject.SavedLocation = new LocationEngine(x, y, z);
                    resultPlayer.PlayerObject.Direction = (Direction)resultPlayer.Direction;
                }

                resultPlayer.PlayerObject.Speed = (ushort)(220 + (2 * (resultPlayer.Level - 1)));
            }

            return resultPlayer;
        }
    }
}
