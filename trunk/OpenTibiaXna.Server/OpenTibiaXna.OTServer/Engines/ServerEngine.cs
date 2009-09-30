using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Items;
using System.Net.Sockets;
using OpenTibiaXna.Helpers.ServerSettings;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Entities;
using System.Net;
using OpenTibiaXna.OTServer.Scripting;
using OpenTibiaXna.Helpers;
using OpenTibiaXna.OTServer.Helpers;
using OpenTibiaXna.OTServer.Logging;

namespace OpenTibiaXna.OTServer.Engines
{
    public class ServerEngine
    {
        static GameWorld gameWorld;

        static TcpListener clientGameListener = new TcpListener(IPAddress.Any,
                                                         SettingsManager.GetGameServerPort());

        public static void Run()
        {
            LoginServerEngine loginServerEngine = LoginServerEngine.Instance;

            Game = new GameObject();
            Connections = new List<Connection>();

            LoggingEngine.LogStart("Initializing Database");
            DatabaseEngine.SetDatabaseType();
            LoggingEngine.LogDone();

            LoggingEngine.LogStart("Initializing Multi-World System");
            gameWorld = GameWorldEngine.Initialize();
            LoggingEngine.LogDone();

            LoggingEngine.LogStart("Loading data");
            DatReader.Load();
            LoggingEngine.LogDone();

            LoggingEngine.LogStart("Loading items.xml");
            ItemObject.LoadItemsXml();
            LoggingEngine.LogDone();

            LoggingEngine.LogStart("Loading map");
            Game.Map.Load();
            LoggingEngine.LogDone();

            LoggingEngine.LogStart("Loading scripts");
            //game.Scripter.Load();
            string errors = OpenTibiaXna.OTServer.Scripting.ScriptManager.LoadAllScripts(Game);
            LoggingEngine.LogDone();

            if (errors.Length > 0)
            {
                LoggingEngine.LogError(new LogErrorException("There were errors when compiling scripts:\n\n" + errors));
            }

            LoggingEngine.LogStart("Listening for clients");
            clientGameListener.Start();
            clientGameListener.BeginAcceptSocket(new AsyncCallback(GameListenerCallback), clientGameListener);
            LoggingEngine.LogDone();
            //}
            //catch (Exception e)
            //{
            //    LogError(e.ToString());
            //}

            while (true)
            {
                bool exit = false;

                switch (ServerCommand)
                {
                    case ServerCommands.Exit:
                        exit = true;
                        break;
                    case ServerCommands.ReloadScripts:
                        ScriptManager.ReloadAllScripts(Game);
                        break;
                    default:
                        LoggingEngine.LogError(new LogErrorException("Unknow server command.")); 
                        break;
                }

                if (exit) break;
            }

            Connections.ForEach(c => c.Close());
            //clientGameListener.Stop();
        }

        private static void GameListenerCallback(IAsyncResult ar)
        {
            Connection connection = new Connection(Game);
            connection.GameListenerCallback(ar);
            Connections.Add(connection);

            clientGameListener.BeginAcceptSocket(new AsyncCallback(GameListenerCallback), clientGameListener);
            LoggingEngine.LogMessage("New client connected to game server.");
        }

        public static GameObject Game { get; set; }

        public static List<Connection> Connections { get; set; }

        public static ServerCommands ServerCommand { get; set; }
    }
}
