using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using OpenTibiaXna.Helpers;
using OpenTibiaXna.OTServer.Scripting;
using OpenTibiaXna.Helpers.ServerSettings;
using OpenTibiaXna.OTServer;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Items;

namespace OpenTibiaXna.OTServer.Engines
{
    public class ServerObject
    {
        static void Main(string[] args)
        {
            new ServerObject().Run();
        }

        Game game;
        GameWorld gameWorld;

        TcpListener clientLoginListener = new TcpListener(IPAddress.Any, 
                                                         SettingsManager.GetLoginServerPort());
        TcpListener clientGameListener = new TcpListener(IPAddress.Any, 
                                                         SettingsManager.GetGameServerPort());
        List<Connection> connections = new List<Connection>();

        static int startTimeInMillis = 0;
        static int startTextLength = 0;

        public void Run()
        {
            int id = -1;
            game = new Game(this);

            //if (!Database.AccountNameExists("creator"))
            //{
            //    id = Database.CreateAccount("creator", "creator");
            //    if (id > 0 && !Database.PlayerNameExists("Account Creator"))
            //        Database.CreatePlayer(id, "Account Creator", game.GenerateAvailableId());
            //}
            //if (!Database.AccountNameExists("1"))
            //{
            //    id = Database.CreateAccount("1", "1");
            //    if (id > 0 && !Database.PlayerNameExists("God"))
            //        Database.CreatePlayer(id, "God", game.GenerateAvailableId());
            //}
            //if (!Database.AccountNameExists("2"))
            //{
            //    id = Database.CreateAccount("2", "2");
            //    if (id > 0 && !Database.PlayerNameExists("Bob"))
            //        Database.CreatePlayer(id, "Bob", game.GenerateAvailableId());
            //}
            //if (!Database.AccountNameExists("3") && !Database.PlayerNameExists("Alice"))
            //{
            //    id = Database.CreateAccount("3", "3");
            //    if (id > 0 && !Database.PlayerNameExists("Alice"))
            //        Database.CreatePlayer(id, "Alice", game.GenerateAvailableId());
            //}

            ////try
            ////{
            LogStart("Initializing Database");
            DatabaseEngine.SetDatabaseType();
            LogDone();

            LogStart("Initializing Multi-World System");
            gameWorld = GameWorldEngine.Initialize();
            LogDone();

            LogStart("Loading data");
            DatReader.Load();
            LogDone();

            LogStart("Loading items.xml");
            ItemObject.LoadItemsXml();
            LogDone();

            LogStart("Loading map");
            game.Map.Load();
            LogDone();

            LogStart("Loading scripts");
            //game.Scripter.Load();
            string errors = OpenTibiaXna.OTServer.Scripting.ScriptManager.LoadAllScripts(game);
            LogDone();

            if (errors.Length > 0)
            {
                Log("There were errors when compiling scripts:\n\n" + errors);
            }

            LogStart("Listening for clients");
            clientLoginListener.Start();
            clientLoginListener.BeginAcceptSocket(new AsyncCallback(LoginListenerCallback), clientLoginListener);
            clientGameListener.Start();
            clientGameListener.BeginAcceptSocket(new AsyncCallback(GameListenerCallback), clientGameListener);
            LogDone();
            //}
            //catch (Exception e)
            //{
            //    LogError(e.ToString());
            //}

            while (true)
            {
                bool exit = false;

                switch (this.ServerCommand)
                {
                    case ServerCommands.Exit:
                        exit = true;
                        break;
                    case ServerCommands.ReloadScripts:
                        ScriptManager.ReloadAllScripts(game);
                        this.ServerCommand = ServerCommands.None;
                        break;
                    case ServerCommands.None:
                        break;
                    default:
                        ServerObject.LogError("Unknow server command.");
                        break;
                }

                if (exit) break;
            }
            connections.ForEach(c => c.Close());
            clientGameListener.Stop();
            clientLoginListener.Stop();
        }

        public static void LogStart(string text)
        {
            string s = DateTime.Now + ": " + text + "...";
            Console.Write(s);
            startTextLength = s.Length;
            startTimeInMillis = System.Environment.TickCount;
        }

        public static void LogDone()
        {
            int elapsed = System.Environment.TickCount - startTimeInMillis;
            string done = "Done";
            string doneTime = "";

            if (elapsed < 1000)
            {
                doneTime = String.Format("({0} ms)", elapsed);
            }
            else
            {
                doneTime = String.Format("({0:0.00} s)", elapsed / 1000.0);
            }

            Console.Write(".".Repeat(Console.WindowWidth - startTextLength - done.Length - 12));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(done);
            Console.ResetColor();
            Console.Write(" ".Repeat(11 - doneTime.Length));
            Console.Write(doneTime);
            Console.WriteLine();
        }

        public static void LogError(string errorText)
        {
            string error = "Error";
            Console.Write(".".Repeat(Console.WindowWidth - startTextLength - error.Length - 12));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
            Console.WriteLine(errorText);
        }

        public static void Log(string text)
        {
            Console.WriteLine(DateTime.Now + ": " + text);
        }

        private void LoginListenerCallback(IAsyncResult ar)
        {
            Connection connection = new Connection(game);
            connection.LoginListenerCallback(ar);
            connections.Add(connection);

            clientLoginListener.BeginAcceptSocket(new AsyncCallback(LoginListenerCallback), clientLoginListener);
            Log("New client connected to login server.");
        }

        private void GameListenerCallback(IAsyncResult ar)
        {
            Connection connection = new Connection(game);
            connection.GameListenerCallback(ar);
            connections.Add(connection);

            clientGameListener.BeginAcceptSocket(new AsyncCallback(GameListenerCallback), clientGameListener);
            Log("New client connected to game server.");
        }

        public ServerCommands ServerCommand { get; set; }
    }
}
