using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

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

        public async Task SendPosition(string roomId, string user, MoveObject move)
        {
            await Clients.All.SendAsync("ReceivePosition", roomId, user, move);
        }
        public async Task SendCommunication(string communication)
        {
            await Clients.All.SendAsync("ReceiveCommunication", communication);
        }


//             color: "w"
            // from: "f2"
            // to: "f3"
            // flags: "n"
            // piece: "p"
            // san: "f3"

        public class MoveObject 
        {
            [JsonProperty("color")]
            public string Color { get; set; }
            [JsonProperty("from")]
            public string From { get; set; }
            [JsonProperty("to")]  
            public string To { get; set; }
            [JsonProperty("flags")]
            public string Flags { get; set; }
            [JsonProperty("piece")]
            public string Piece  { get; set; }
            [JsonProperty("san")]
            public string San { get; set; }
        }
    }
}