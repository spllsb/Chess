using System;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Chess.Infrastructure.Hubs
{
    public class ChessMatchHub : Hub
    {
        private readonly IChessGameService _chessGameService;
        private readonly IUserService _userService;

        public ChessMatchHub(IChessGameService chessGameService,
                            IUserService userService)
        {
            _chessGameService = chessGameService;
            _userService = userService;
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
        // public async Task JoinChessMatch(string chessGameId){
        //     // await this.Groups.AddToGroupAsync(this.Context.ConnectionId, chessGameId);
        //     // Console.WriteLine("--> Connection to group " + chessGameId);
        // }

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
            if(_chessGameService.CountOpponent() == 0)
            {
                await _chessGameService.AddToWaitingList(userId, this.Context.ConnectionId);
                Console.WriteLine("--> Connected user witch connectionId " + userId + " was added to waiting player list");
                return "Added to waiting list";
            }
            else
            {
                //NA tym etapie, powinno być dołczenie do grup i zwrotka do obu graczy ze się zaczeło
                Console.WriteLine("--> Searching good opponent");
                var opponent = await _chessGameService.GetPlayerFromWaitingList();
                Console.WriteLine("--> Connected user witch userId " + userId + " found player with id " + opponent.PlayerId + " " + opponent.ConnectionId);
                var room = Room.CreateRoom();
                
                Task t1 = Groups.AddToGroupAsync(userId, room.Id.ToString());
                Task t2 = Groups.AddToGroupAsync(opponent.PlayerId.ToString(), room.Id.ToString());
                
                await _chessGameService.RemoveFromWaitingList(opponent);

                t1.Wait();
                t2.Wait();

                return "Player found game";
            }

        }

        
        public async Task<string> SearchRandomChessGame(string userId){
            Console.WriteLine("--> Connection id: " + this.Context.ConnectionId + " and userId " + userId + " serching game");
            if(_chessGameService.CountOpponent() == 0)
            {
                await _chessGameService.AddToWaitingList(userId, this.Context.ConnectionId);
                Console.WriteLine("--> Connected user witch connectionId " + userId + " was added to waiting player list");
                return "Added to waiting list";
            }
            else
            {
                //NA tym etapie, powinno być dołczenie do grup i zwrotka do obu graczy ze się zaczeło
                Console.WriteLine("--> Searching good opponent");
                var opponent = await _chessGameService.GetPlayerFromWaitingList();
                Console.WriteLine("--> Connected user witch userId " + userId + " found player with id " + opponent.PlayerId + " " + opponent.ConnectionId);
                var room = Room.CreateRoom();
                Console.WriteLine("--> Create new room with id " + room.Id.ToString());

                Task t1 = Groups.AddToGroupAsync(this.Context.ConnectionId, room.Id.ToString());
                Task t2 = Groups.AddToGroupAsync(opponent.ConnectionId.ToString(), room.Id.ToString());
                
                t1.Wait();
                t2.Wait();
                
                await _chessGameService.RemoveFromWaitingList(opponent);
                

                await Clients.Group(room.Id.ToString()).SendAsync("ReceiveRoom", room.Id.ToString());
                return "Player found game";
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