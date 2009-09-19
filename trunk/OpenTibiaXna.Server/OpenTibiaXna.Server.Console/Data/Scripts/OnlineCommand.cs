using OpenTibiaXna.OTServer;
using OpenTibiaXna.OTServer.Scripting;
using OpenTibiaXna.OTServer.Entities;
using OpenTibiaXna.OTServer.Objects;

public class OnlineCommand : IScript
{
    Game game;
    public bool Start(Game game)
    {
        this.game = game;
        game.BeforeCreatureSpeech += BeforeCreatureSpeech;
        return true;
    }

    public bool BeforeCreatureSpeech(Creature creature, Speech speech)
    {
        if (creature.IsPlayer && speech.Message.ToLower().Equals("/online"))
        {
            string online = "";
            foreach (PlayerObject player in game.GetPlayers())
            {
                if (online.Length > 0)
                {
                    online += ", ";
                }
                online += player.Name;
            }
            ((PlayerObject)creature).Connection.SendTextMessage(TextMessageType.EventDefault, "Online: " + online);
            return false;
        }
        return true;
    }

    public bool Stop()
    {
        game.BeforeCreatureSpeech -= BeforeCreatureSpeech;
        return true;
    }
}