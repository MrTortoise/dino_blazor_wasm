using System;
using System.Collections.Generic;
using DinoGame.Game.GameLobby;
using EventBase.Client;
using Newtonsoft.Json;

namespace DinoGame.Game.Spec
{
    public class CreateGameWorker : ITurnEventsIntoCommand
    {
        private const string DateTimeFormat = "YYYYMMDD";

        public void Load(IEnumerable<StreamEvent> events)
        {
            throw new System.NotImplementedException();
        }

        public void SubscribeTo(IEventStore eventStore)
        {
            string date = DateTime.Today.ToString(DateTimeFormat);
            
            eventStore.SubscribeToStream($"gameLobby-{date}", @event =>
            {
                var e = (CreateGameRequestApproved) @event.Event;
                var streamName = $"game-{e.GameId}";

                var createStreamEvent = new CreateStreamEvent(streamName, StreamPositions.CreateNewStream, new GameCreated(e.GameId));
                eventStore.Append(createStreamEvent);
            });
        }
    }

    public class GameCreated : Event
    {
        public string GameId { get; }

        [JsonConstructor]
        public GameCreated(string gameId, string id) : base(id)
        {
            GameId = gameId;
        }

        public GameCreated(string gameId)
        {
            GameId = gameId;
        }
    }
}