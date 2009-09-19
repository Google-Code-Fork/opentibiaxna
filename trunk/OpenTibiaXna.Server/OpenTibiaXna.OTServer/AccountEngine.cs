using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Entities;
using System.Net;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Engines
{
    public class AccountEngine
    {
        public static IEnumerable<CharacterListItem> GetCharacterList(Account account)
        {
            foreach (Player player in account.Player)
                yield return new CharacterListItem(
                    player.Name,
                    player.GameWorld.GameWorldName,
                    player.GameWorld.GameWorldIp,
                    player.GameWorld.GamePort);
        }
    }
}
