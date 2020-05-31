using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Chess.Infrastructure.Hubs
{
    public class ChessMatchHub : Hub
    {
        private readonly IChessGameService _chessGameService;

        public ChessMatchHub(IChessGameService chessGameService)
        {
            _chessGameService = chessGameService;
        }

        public override Task OnConnectedAsync() 
        {
            Console.WriteLine("--> Connection Established " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        // public async Task SendPosition(string roomId, string user, MoveObject move)
        // {
        //     await Clients.All.SendAsync("ReceivePosition", roomId, user, move);
        // }
        // public async Task SendCommunication(string communication)
        // {
        //     await Clients.All.SendAsync("ReceiveCommunication", communication);
        // }
        public async Task JoinChessMatch(string chessGameId){
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, chessGameId);
            Console.WriteLine("--> Connection to group " + chessGameId);
        }

        public async Task SendPosition(string chessGameId, string user, MoveObject move)
        {
            await Clients.Group(chessGameId).SendAsync("ReceivePosition", chessGameId, user, move);
        }
        public async Task SendCommunication(string communication)
        {
            await Clients.All.SendAsync("ReceiveCommunication", communication);
        }

        // public async Task ChoosePieceColor(string )
        public string GetConnectionId(){
            return this.Context.ConnectionId;
        }

        public async Task<string> SearchChessGame(string userId){
            Console.WriteLine("--> Connection id: " + this.Context.ConnectionId + " and userId " + userId + " serching game");
            if(await _chessGameService.GetPlayerFromWaitingList() == null)
            {
                await _chessGameService.AddToWaitingList(userId);
                Console.WriteLine("--> Connected user witch connectionId " + userId + " was added to waiting player list");
                return "Added to waiting list";
            }
            else
            {
                Console.WriteLine("--> Searching good opponent");
                var opponent = await _chessGameService.GetPlayerFromWaitingList();
                Console.WriteLine("--> Connected user witch userId " + userId + " found player with id " + opponent.PlayerId + " " + opponent.Username);
                return "Player " + opponent.PlayerId + " " + opponent.Username + "was found";
            }
        }


            //color: "w"
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