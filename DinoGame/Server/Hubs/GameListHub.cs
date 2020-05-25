using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DinoGame.Server.Hubs
{
    public class GameListHub : Hub
    {
        public async Task GameListUpdate(string game)
        {
            await Clients.All.SendAsync("GameListUpdate", game);
        }
    }
}
