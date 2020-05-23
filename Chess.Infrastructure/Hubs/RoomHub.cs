using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;



namespace Chess.Infrastructure.Hubs
{
    public class RoomHub : Hub
    {
        public override Task OnConnectedAsync() 
        {


            Console.WriteLine("--> Connection Established " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public async Task SendCommunication1(string communication)
        {
            await Clients.All.SendAsync("ReceiveCommunication1", communication);
        } 

           public async Task ConnectToRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("TestMessage", Context.User.Identity.Name + " joined.");
        }
    }
}