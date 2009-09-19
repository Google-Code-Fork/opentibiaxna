using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.Helpers.ServerSettings;
using System.Net;

namespace OpenTibiaXna.OTServer.Engines
{
    public class GameWorldEngine
    {
        public static GameWorld Initialize()
        {
            GameWorld gameWorld = GameWorld.GetGameWorldBy(SettingsManager.GetGameWorldName());

            if (gameWorld == null)
            {
                gameWorld = new GameWorld();
                gameWorld.GameWorldName = SettingsManager.GetGameWorldName();
                gameWorld.GameWorldIp = IPAddress.Parse("127.0.0.1").GetAddressBytes();
                gameWorld.GamePort = (ushort)SettingsManager.GetGameServerPort();

                GenericDatabase.Save(gameWorld);
            }

            return gameWorld;
        }
    }
}
