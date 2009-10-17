using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using OpenTibiaXna.Helpers;
using OpenTibiaXna.OTServer.Packets;
using OpenTibiaXna.OTServer.Packets.Client;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.OTServer.Engines;
using OpenTibiaXna.OTServer.Packets.Server;
using OpenTibiaXna.OTServer.Logging;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Helpers;

namespace OpenTibiaXna.OTServer.Engines
{
    public class ConnectionEngine
    {
        #region Variables

        Socket socket;
        NetworkStream stream;
        NetworkMessageEngine inMessage = new NetworkMessageEngine(0);
        uint[] xteaKey = new uint[4];
        bool remove = false;
        HashSet<uint> knownCreatures = new HashSet<uint>();
        Queue<Direction> walkDirections;

        #endregion

        #region Constructor

        public ConnectionEngine(GameObject game)
        {
            this.Game = game;
        }

        #endregion

        #region Properties

        public Player Player { get; set; }

        public PlayerObject PlayerObject 
        { 
            get { return Player.PlayerObject; }
            set { Player.PlayerObject = value; }
        }

        public GameObject Game { get; set; }

        public long AccountId { get; set; }

        public bool ShouldRemove
        {
            get { return remove; }
        }

        public string Ip
        {
            get
            {
                return socket.RemoteEndPoint.ToString();
            }
        }

        #endregion

        #region Callbacks

        public void LoginListenerCallback(IAsyncResult ar)
        {
            TcpListener clientListener = (TcpListener)ar.AsyncState;
            socket = clientListener.EndAcceptSocket(ar);
            stream = new NetworkStream(socket);

            stream.BeginRead(inMessage.Buffer, 0, 2,
                new AsyncCallback(ClientReadFirstCallBack), null);
        }

        public void GameListenerCallback(IAsyncResult ar)
        {
            TcpListener gameListener = (TcpListener)ar.AsyncState;
            socket = gameListener.EndAcceptSocket(ar);
            stream = new NetworkStream(socket);

            SendConnectionPacket();

            stream.BeginRead(inMessage.Buffer, 0, 2,
                new AsyncCallback(ClientReadFirstCallBack), null);
        }

        private void ClientReadFirstCallBack(IAsyncResult ar)
        {
            if (!EndRead(ar)) return;

            byte protocol = inMessage.GetByte(); // protocol id (1 = login, 2 = game)

            if (protocol == 0x01)
            {
                AccountPacket accountPacket = AccountPacket.Parse(inMessage);
                xteaKey = accountPacket.XteaKey;

                Account account = AccountEngine.GetAccountBy(accountPacket.Name, Hash.SHA256Hash(accountPacket.Password));

                if (account != null)
                {
                        this.AccountId = account.AccountId;
                        SendCharacterList(
                            "SharpOT.Properties.Settings.Default.MessageOfTheDay",
                            999,
                            AccountEngine.GetCharacterList(account)
                        );
                }
                else
                {
                    this.SendDisconnect("Account name or password incorrect.");
                }

                Close();
            }
            else if (protocol == 0x0A)
            {
                ParseLoginPacket(inMessage);

                stream.BeginRead(inMessage.Buffer, 0, 2,
                    new AsyncCallback(ClientReadCallBack), null);
            }
        }

        private void ClientReadCallBack(IAsyncResult ar)
        {
            if (!EndRead(ar))
            {
                // Client crashed or disconnected
                Game.PlayerLogout(Player);
                return;
            }

            inMessage.XteaDecrypt(xteaKey);
            ushort length = inMessage.GetUInt16();
            byte type = inMessage.GetByte();

            ParseClientPacket((ClientPacketType)type, inMessage);
            
            if (!remove)
            {
                stream.BeginRead(inMessage.Buffer, 0, 2,
                    new AsyncCallback(ClientReadCallBack), null);
            }
        }

        private bool EndRead(IAsyncResult ar)
        {
            int read = stream.EndRead(ar);

            if (read == 0)
            {
                // client disconnected
                Close();
                return false;
            }

            int size = (int)BitConverter.ToUInt16(inMessage.Buffer, 0) + 2;

            while (read < size)
            {
                if (stream.CanRead)
                    read += stream.Read(inMessage.Buffer, read, size - read);
            }
            inMessage.Length = size;

            inMessage.Position = 0;

            inMessage.GetUInt16(); // total length
            inMessage.GetUInt32(); // adler

            return true;
        }

        #endregion

        #region Parse

        private void ParseLoginPacket(NetworkMessageEngine message)
        {
            LoginPacket loginPacket = LoginPacket.Parse(message);
            xteaKey = loginPacket.XteaKey;

            Account account = Game.CheckAccount(this, loginPacket.AccountName, loginPacket.Password);

            if (account.AccountId >= 0)
            {
                this.AccountId = account.AccountId;
                Game.ProcessLogin(this, loginPacket.CharacterName);
            }
            else
            {
                Close();
            }
        }

        private void ParseClientPacket(ClientPacketType type, NetworkMessageEngine message)
        {
            switch (type)
            {
                case ClientPacketType.Logout:
                    ParseLogout();
                    break;
                //case ClientPacketType.ItemMove:
                //case ClientPacketType.ShopBuy:
                //case ClientPacketType.ShopSell:
                //case ClientPacketType.ShopClose:
                //case ClientPacketType.ItemUse:
                //case ClientPacketType.ItemUseOn:
                //case ClientPacketType.ItemRotate:
                case ClientPacketType.LookAt:
                    ParseLookAt(message);
                    break;
                case ClientPacketType.PlayerSpeech:
                    ParsePlayerSpeech(message);
                    break;
                case ClientPacketType.ChannelList:
                    SendChannelList(PlayerObject);
                    break;
                case ClientPacketType.ClientChannelOpen:
                    ParseClientChannelOpen(message);
                    break;
                case ClientPacketType.ChannelClose:
                    ParseChannelClose(message);
                    break;
                //case ClientPacketType.Attack:
                //case ClientPacketType.Follow:
                //case ClientPacketType.CancelMove:
                //case ClientPacketType.ItemUseBattlelist:
                //case ClientPacketType.ContainerClose:
                //case ClientPacketType.ContainerOpenParent:
                case ClientPacketType.TurnNorth:
                    Game.CreatureTurn(PlayerObject, Direction.North);
                    break;
                case ClientPacketType.TurnWest:
                    Game.CreatureTurn(PlayerObject, Direction.East);
                    break;
                case ClientPacketType.TurnSouth:
                    Game.CreatureTurn(PlayerObject, Direction.South);
                    break;
                case ClientPacketType.TurnEast:
                    Game.CreatureTurn(PlayerObject, Direction.West);
                    break;
                case ClientPacketType.AutoWalk:
                    ParseAutoWalk(message);
                    break;
                case ClientPacketType.AutoWalkCancel:
                    ParseAutoWalkCancel();
                    break;                 
                case ClientPacketType.VipAdd:
                    ParseVipAdd(message);
                    break;
                case ClientPacketType.VipRemove:
                    ParseVipRemove(message);
                    break;
                case ClientPacketType.RequestOutfit:
                    SendOutfitWindow();
                    break;
                case ClientPacketType.ChangeOutfit:
                    ParseChangeOutfit(message);
                    break;
                //case ClientPacketType.Ping:
                case ClientPacketType.FightModes:
                    ParseFightModes(message);
                    break;
                //case ClientPacketType.ContainerUpdate:
                //case ClientPacketType.TileUpdate:
                case ClientPacketType.PrivateChannelOpen:
                    ParsePrivateChannelOpen(message);
                    break;
                //case ClientPacketType.NpcChannelClose:
                //    break;
                case ClientPacketType.MoveNorth:
                    Game.CreatureMove(PlayerObject,  Direction.North);
                    break;
                case ClientPacketType.MoveEast:
                    Game.CreatureMove(PlayerObject,  Direction.East);
                    break;
                case ClientPacketType.MoveSouth:
                    Game.CreatureMove(PlayerObject,  Direction.South);
                    break;
                case ClientPacketType.MoveWest:
                    Game.CreatureMove(PlayerObject,  Direction.West);
                    break;
                case ClientPacketType.MoveNorthEast:
                    Game.CreatureMove(PlayerObject,  Direction.NorthEast);
                    break;
                case ClientPacketType.MoveSouthEast:
                    Game.CreatureMove(PlayerObject,  Direction.SouthEast);
                    break;
                case ClientPacketType.MoveSouthWest:
                    Game.CreatureMove(PlayerObject,  Direction.SouthWest);
                    break;
                case ClientPacketType.MoveNorthWest:
                    Game.CreatureMove(PlayerObject,  Direction.NorthWest);
                    break;
                default:
                    LoggingEngine.LogError(new LogErrorException("Unhandled packet from " + PlayerObject.ToString() + ": " + type));
                    break;
            }
        }

        public void ParseAutoWalk(NetworkMessageEngine message)
        {
            AutoWalkPacket packet = AutoWalkPacket.Parse(message);
            walkDirections = packet.Directions;
            DoAutoWalk();
        }

        public void ParseAutoWalkCancel()
        {
            Game.WalkCancel(PlayerObject);
        }

        public void ParseLogout()
        {
            Game.PlayerLogout(Player);
        }

        public void ParsePlayerSpeech(NetworkMessageEngine message)
        {
            PlayerSpeechPacket packet = PlayerSpeechPacket.Parse(message);

            Game.CreatureSpeech(this.PlayerObject, packet.Speech);
        }

        public void ParseClientChannelOpen(NetworkMessageEngine message)
        {
            ClientChannelOpenPacket packet = ClientChannelOpenPacket.Parse(message);
            Game.ChannelOpen(PlayerObject, packet.Channel);
        }

        public void ParseChannelClose(NetworkMessageEngine message)
        {
            ChannelClosePacket packet = ChannelClosePacket.Parse(message);
            Game.ChannelClose(PlayerObject, packet.Channel);
        }
        
        public void ParseVipAdd(NetworkMessageEngine message)
        {
            VipAddPacket packet = VipAddPacket.Parse(message);
            Game.VipAdd(PlayerObject, packet.Name);
        }

        public void ParseVipRemove(NetworkMessageEngine message)
        {
            VipRemovePacket packet = VipRemovePacket.Parse(message);
            Game.VipRemove(PlayerObject, packet.Id);
        }

        public void ParseChangeOutfit(NetworkMessageEngine message)
        {
            ChangeOutfitPacket packet = ChangeOutfitPacket.Parse(message);
            Game.PlayerChangeOutfit(PlayerObject, packet.Outfit);
        }

        public void ParseFightModes(NetworkMessageEngine message)
        {
            FightModesPacket packet = FightModesPacket.Parse(message);
            PlayerObject.FightMode = (FightMode)packet.FightMode;
            PlayerObject.ChaseMode = packet.ChaseMode;
            PlayerObject.SafeMode = packet.SafeMode;
        }

        public void ParsePrivateChannelOpen(NetworkMessageEngine message)
        {
            PrivateChannelOpenPacket packet = PrivateChannelOpenPacket.Parse(message);
            Game.PrivateChannelOpen(PlayerObject, packet.Receiver);
        }

        public void ParseLookAt(NetworkMessageEngine message)
        {
            LookAtPacket packet = LookAtPacket.Parse(message);
            Game.PlayerLookAt(PlayerObject, packet.Id, packet.Location, packet.StackPosition);
        }
        
        #endregion

        #region Send

        private void SendConnectionPacket()
        {
            NetworkMessageEngine message = new NetworkMessageEngine();

            GameServerConnectPacket.Add(message);

            Send(message, false);
        }

        public void SendCharacterList(string motd, ushort premiumDays, IEnumerable<CharacterListItem> chars)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();

            if (motd != string.Empty)
            {
                MessageOfTheDayPacket.Add(
                    message,
                    motd
                );
            }
            CharacterListPacket.Add(
                message,
                chars,
                premiumDays
            );

            Send(message);
        }

        public void SendInitialPacket()
        {
            NetworkMessageEngine message = new NetworkMessageEngine();

            SelfAppearPacket.Add(
                message,
                PlayerObject.Id,
                true
            );

            MapDescriptionPacket.Add(
                this,
                message,
                PlayerObject.Tile.Location
            );

            EffectPacket.Add(
                message,
                Effect.EnergyDamage,
                PlayerObject.Tile.Location
            );

            // Inventory
            //message.AddBytes("78 01 1A 0D 78 03 26 0B 78 04 1E 0D 78 05 52 0D 78 06 D3 0C 78 08 E0 0D 78 0A 9B 0D".ToBytesAsHex());

            WorldLightPacket.Add(
                message,
                LightLevel.World,
                LightColor.White
            );

            CreatureLightPacket.Add(
                message,
                PlayerObject.Id,
                LightLevel.None,
                LightColor.None
            );

            // TODO: Save last login.
            TextMessagePacket.Add(
                message,
                TextMessageType.EventDefault,
                "Welcome to Utopia! Last login: yesterday."
            );

            PlayerStatusPacket.Add(
                message,
                PlayerObject.Health,
                PlayerObject.MaxHealth,
                PlayerObject.Capacity,
                PlayerObject.Experience,
                PlayerObject.Level,
                0, // TODO: level system
                PlayerObject.Mana,
                PlayerObject.MaxMana,
                PlayerObject.MagicLevel,
                0,
                0,
                0
            );


            // Player skills
            //message.AddBytes("A1 0A 02 0A 00 0E 44 0B 62 0A 0D 0F 3E 13 26".ToBytesAsHex());

            // Fight modes
            //message.AddBytes("A0 02 00 01".ToBytesAsHex());

            Send(message);
        }

        public void SendOutfitWindow()
        {
            NetworkMessageEngine message = new NetworkMessageEngine();

            // TODO: put somewhere else, xml?
            List<OutfitObject> outfits;

            if (PlayerObject.Gender == Gender.Male)
            {
                outfits = new List<OutfitObject>
                {
                    new OutfitObject("Citizen", 128, 7),
                    new OutfitObject("Hunter", 129, 7),
                    new OutfitObject("Mage", 130, 7),
                    new OutfitObject("Knight", 131, 7),
                    new OutfitObject("Nobleman", 132, 7),
                    new OutfitObject("Summoner", 133, 7),
                    new OutfitObject("Warrior", 134, 7),
                    new OutfitObject("Barbarian", 143, 7),
                    new OutfitObject("Druid", 144, 7),
                    new OutfitObject("Wizard", 145, 7),
                    new OutfitObject("Oriental", 146, 7),
                    new OutfitObject("Pirate", 151, 7),
                    new OutfitObject("Assassin", 152, 7),
                    new OutfitObject("Beggar", 153, 7),
                    new OutfitObject("Shaman", 154, 7),
                    new OutfitObject("Norseman", 251, 7),
                    new OutfitObject("Nightmare", 268, 7),
                    new OutfitObject("Jester", 273, 7),
                    new OutfitObject("Brotherhood", 278, 7),
                    new OutfitObject("Demonhunter", 289, 7),
                    new OutfitObject("Yalaharian", 325, 7),
                    new OutfitObject("Wedding", 328, 7),
                    new OutfitObject("Gamemaster", 75, 7),
                    new OutfitObject("Old Com. Manager", 266, 7),
                    new OutfitObject("Com. Manager", 302, 7)
                };
            }
            else
            {
                outfits = new List<OutfitObject>
                {
                    new OutfitObject("Citizen", 136, 7),
                    new OutfitObject("Hunter", 137, 7),
                    new OutfitObject("Mage", 138, 7),
                    new OutfitObject("Knight", 139, 7),
                    new OutfitObject("Noblewoman", 140, 7),
                    new OutfitObject("Summoner", 141, 7),
                    new OutfitObject("Warrior", 142, 7),
                    new OutfitObject("Barbarian", 147, 7),
                    new OutfitObject("Druid", 148, 7),
                    new OutfitObject("Wizard", 149, 7),
                    new OutfitObject("Oriental", 150, 7),
                    new OutfitObject("Pirate", 155, 7),
                    new OutfitObject("Assassin", 156, 7),
                    new OutfitObject("Beggar", 157, 7),
                    new OutfitObject("Shaman", 158, 7),
                    new OutfitObject("Norsewoman", 252, 7),
                    new OutfitObject("Nightmare", 269, 7),
                    new OutfitObject("Jester", 270, 7),
                    new OutfitObject("Brotherhood", 279, 7),
                    new OutfitObject("Demonhunter", 288, 7),
                    new OutfitObject("Yalaharian", 324, 7),
                    new OutfitObject("Wedding", 329, 7),
                    new OutfitObject("Gamemaster", 75, 7),
                    new OutfitObject("Old Com. Manager", 266, 7),
                    new OutfitObject("Com. Manager", 302, 0)
                };
            }
            OutfitWindowPacket.Add(
                message,
                PlayerObject,
                outfits
            );

            Send(message);
        }

        public void SendStatus()
        {
            NetworkMessageEngine outMessage = new NetworkMessageEngine();
            PlayerStatusPacket.Add(
               outMessage,
                PlayerObject.Health,
                PlayerObject.MaxHealth,
                PlayerObject.Capacity,
                PlayerObject.Experience,
                PlayerObject.Level,
                0, // TODO: level system
                PlayerObject.Mana,
                PlayerObject.MaxMana,
                PlayerObject.MagicLevel,
                0,
                0,
                0
            );

            Send(outMessage);
        }

        public void SendCreatureChangeOutfit(CreatureObject creature)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();

            CreatureChangeOutfitPacket.Add(
                message,
                creature
            );

            Send(message);
        }

        public void SendCreatureMove(LocationEngine fromLocation, byte fromStackPosition, LocationEngine toLocation)
        {
            NetworkMessageEngine outMessage = new NetworkMessageEngine();

            CreatureMovePacket.Add(
                outMessage,
                fromLocation,
                fromStackPosition,
                toLocation
            );

            Send(outMessage);
        }

        public void SendCreatureUpdateHealth(CreatureObject creature)
        {
            NetworkMessageEngine outMessage = new NetworkMessageEngine();

            CreatureHealthPacket.Add(
                outMessage,
                creature.Id,
                creature.HealthPercent);

            Send(outMessage);
        }

        public void SendCreatureLogout(CreatureObject creature)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            EffectPacket.Add(
                message,
                Effect.Puff, // TODO: find the new value for poof
                creature.Tile.Location
            );
            TileRemoveThingPacket.Add(
                message,
                creature.Tile.Location,
                creature.Tile.GetStackPosition(creature)
            );
            Send(message);
        }

        public void SendTileRemoveThing(LocationEngine fromLocation, byte fromStackPosition)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            TileRemoveThingPacket.Add(
                message, 
                fromLocation, 
                fromStackPosition
            );
            Send(message);
        }

        public void SendCreatureAppear(CreatureObject creature)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();

            EffectPacket.Add(
                message,
                Effect.EnergyDamage,
                creature.Tile.Location
            );

            uint remove;
            bool known = IsCreatureKnown(creature.Id, out remove);
            TileAddCreaturePacket.Add(
                message, 
                creature.Tile.Location, 
                creature.Tile.GetStackPosition(creature), 
                creature, 
                known, 
                remove
            );

            Send(message);
        }

        public void SendTileAddCreature(CreatureObject creature)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            uint remove;
            bool known = IsCreatureKnown(creature.Id, out remove);
            TileAddCreaturePacket.Add(
                message, 
                creature.Tile.Location, 
                creature.Tile.GetStackPosition(creature), 
                creature, 
                known, 
                remove
            );
            Send(message);
        }

        public void SendPlayerMove(LocationEngine fromLocation, byte fromStackPosition, LocationEngine toLocation)
        {
            NetworkMessageEngine outMessage = new NetworkMessageEngine();

            if (fromLocation.Z == 7 && toLocation.Z >= 8)
            {
                TileRemoveThingPacket.Add(
                    outMessage, 
                    fromLocation, 
                    fromStackPosition
                );
            }
            else
            {
                CreatureMovePacket.Add(
                    outMessage,
                    fromLocation,
                    fromStackPosition,
                    toLocation
                );
            }

            //floor change down
            if (toLocation.Z > fromLocation.Z)
            {
                MapFloorChangeDownPacket.Add(
                    this,
                    outMessage,
                    fromLocation,
                    fromStackPosition,
                    toLocation
                );
            }
            //floor change up
            else if (toLocation.Z < fromLocation.Z)
            {
                MapFloorChangeUpPacket.Add(
                    this,
                    outMessage, 
                    fromLocation, 
                    fromStackPosition, 
                    toLocation
                );
            }

            MapSlicePacket.Add(
                this,
                outMessage,
                fromLocation,
                toLocation
            );

            Send(outMessage);
        }

        public void SendCreatureSpeech(CreatureObject creature, SpeechType speechType, string message)
        {

            NetworkMessageEngine outMessage = new NetworkMessageEngine();
            CreatureSpeechPacket.Add(
                outMessage,
                creature.Name,
                1,
                speechType,
                message,
                creature.Tile.Location,
                ChatChannel.None,
                0000
            );
            Send(outMessage);
        }

        public void SendChannelSpeech(string sender, SpeechType type, ChatChannel channelId, string message)
        {
            NetworkMessageEngine outMessage = new NetworkMessageEngine();
            CreatureSpeechPacket.Add(
                outMessage,
                sender,
                1,
                type,
                message,
                null,
                channelId,
                0
            );
            Send(outMessage);
        }   

        public void SendCreatureTurn(CreatureObject creature)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            CreatureTurnPacket.Add(
                message,
                creature
            );
            Send(message);
        }

        public void SendCancelWalk()
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            PlayerWalkCancelPacket.Add(
                message,
                PlayerObject.Direction
            );
            Send(message);
        }

        public void SendDisconnect(string reason)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            message.AddByte((byte)ServerPacketType.Disconnect);
            message.AddString(reason);
            Send(message);
        }

        public void SendChannelOpenPrivate(string name)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            ChannelOpenPrivatePacket.Add(
                message,
                name
            );
            Send(message);
        }

        public void SendChannelList(PlayerObject player)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            ChannelListPacket.Add(
                message,
                player.ChannelList
            );
            Send(message);
        }

        public void SendChannelOpen(Channel channel)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            ChannelOpenPacket.Add(
                message,
                channel.Id,
                channel.Name
            );
            Send(message);
        }

        public void SendTextMessage(TextMessageType type, string text)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            TextMessagePacket.Add(
                message,
                type,
                text
            );
            Send(message);
        }

        public void SendVipState(uint id, string name, bool loggedIn)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            VipStatePacket.Add(
                message,
                id,
                name,
                Convert.ToByte(loggedIn)
            );
            Send(message);
        }

        public void SendVipLogin(uint id)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            VipLoginPacket.Add(
                message,
                id
            );
            Send(message);
        }

        public void SendVipLogout(uint id)
        {
            NetworkMessageEngine message = new NetworkMessageEngine();
            VipLogoutPacket.Add(
                message,
                id
            );
            Send(message);
        }

        public void Send(NetworkMessageEngine message)
        {
            Send(message, true);
        }

        public void Send(NetworkMessageEngine message, bool useEncryption)
        {
            if (useEncryption)
                message.PrepareToSend(xteaKey);
            else
                message.PrepareToSendWithoutEncryption();

            stream.BeginWrite(message.Buffer, 0, message.Length, null, null);
        }

        #endregion

        #region Other

        public bool IsCreatureKnown(uint id, out uint removeId)
        {
            if (knownCreatures.Contains(id))
            {
                removeId = 0;
                return true;
            }
            else
            {
                // TODO: Fix this logic, as it is it never removes
                knownCreatures.Add(id);
                removeId = 0;
                return false;
            }
        }

        public void Close()
        {
            remove = true;
            stream.Close();
            socket.Close();
        }

        #endregion

        #region Private

        private void DoAutoWalk()
        {
            if (walkDirections.Count > 0)
            {
                Direction direction = walkDirections.Dequeue();
                Game.CreatureMove(PlayerObject, direction);
                if (walkDirections.Count > 0)
                {
                    Scheduler.AddTask(
                        this.DoAutoWalk,
                        null,
                        (int)PlayerObject.GetStepDuration());
                }
            }
        }

        #endregion
    }
}
