using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Chess.Infrastructure.Hubs
{
    public class ChessMatchHub : Hub
    {
        public override Task OnConnectedAsync() 
        {
            Console.WriteLine("--> Connection Established " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}