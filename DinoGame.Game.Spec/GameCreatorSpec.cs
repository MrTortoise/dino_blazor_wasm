using System;
using System.Collections.Generic;
using DinoGame.Game.GameLobby;
using EventBase.Client;
using Xunit;

namespace DinoGame.Game.Spec
{
    public class GameCreatorSpec : WorkerTestBase
    {
        [Fact]
        public void WhenCreateGameRequestApproved_GameCreated()
        {
            string gameId = "game1";
            var streamName = $"gameLobby-{DateTime.Today.ToString("YYYYMMDD")}";
            Given(new CreateGameWorker());
            When(new CreateStreamEvent(streamName, 0, new CreateGameRequestApproved(gameId)));
            Then($"game-{gameId}", e =>
            {
                Assert.IsType<GameCreated>(e.GetOriginatingEvent);
                var ut = (GameCreated)e.Event;
                Assert.Equal(gameId, ut.GameId);
            });
        }
    }
}
