using EventBase.Client;
using Newtonsoft.Json;

namespace DinoGame.Game.GameLobby
{
    public class CreateGameRequestApproved : Event
    {
        public string GameId { get; }

        [JsonConstructor]
        public CreateGameRequestApproved(string gameId, string id) : base(id)
        {
            GameId = gameId;
        }

        public CreateGameRequestApproved(string gameId) : base()
        {
            GameId = gameId;
        }
    }
}