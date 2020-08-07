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
        private readonly IFileProvider _fileProvider;
        private readonly IUserService _userService;

        public ChessMatchHub(IChessGameService chessGameService,
                            IUserService userService,
                            IFileProvider fileProvider)
        {
            _chessGameService = chessGameService;
            _userService = userService;
            _fileProvider = fileProvider;
        }

        public override async Task OnConnectedAsync() 
        {
            Console.WriteLine("--> Connection Established " + Context.ConnectionId);
            // await Clients.Client(this.Context.ConnectionId).SendAsync("ReceiveConnID", this.Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine(exception);
            return base.OnDisconnectedAsync(exception);
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

        public async Task SendPosition(string chessGameId, MoveObject move)
        {
            await Clients.Group(chessGameId).SendAsync("ReceivePosition", chessGameId, Context.User.Identity.Name, move);
        }
        public async Task SendCommunication(string communication, string groupId)
        {
            await Clients.Group(groupId).SendAsync("ReceiveCommunication", communication);
        }

        public async Task EndGame(string pgn)
        {
            //TODO plik powinien zawierać loginy graczy
            await _fileProvider.SaveFile(@"C:\Users\spllsb\Desktop\Moje pliki\Szachy\RozegranePartieSzachowe\", DateTime.Now.ToString("yyyyddMMHHmmss") + ".pgn",pgn);
        }

        // public async Task ChoosePieceColor(string )
        public string GetConnectionId(){
            return this.Context.ConnectionId;
        }

        // public async Task<string> SearchChessGame(string userId){
        //     Console.WriteLine("--> Connection id: " + this.Context.ConnectionId + " and userId " + userId + " serching game");
        //     if(_chessGameService.CountOpponent() == 0)
        //     {
        //         await _chessGameService.AddToWaitingList(userId, this.Context.ConnectionId);
        //         Console.WriteLine("--> Connected user witch connectionId " + userId + " was added to waiting player list");
        //         return "Added to waiting list";
        //     }
        //     else
        //     {
        //         //NA tym etapie, powinno być dołczenie do grup i zwrotka do obu graczy ze się zaczeło
        //         Console.WriteLine("--> Searching good opponent");
        //         var opponent = await _chessGameService.GetPlayerFromWaitingList();
        //         Console.WriteLine("--> Connected user witch userId " + userId + " found player with id " + opponent.PlayerId + " " + opponent.ConnectionId);
        //         var room = Room.CreateRoom();
                
        //         Task t1 = Groups.AddToGroupAsync(userId, room.Id.ToString());
        //         Task t2 = Groups.AddToGroupAsync(opponent.PlayerId.ToString(), room.Id.ToString());
                
        //         await _chessGameService.RemoveFromWaitingList(opponent);
                
        //         t1.Wait();
        //         t2.Wait();

        //         return "Player found game";
        //     }

        // }

        
        public async Task<string> SearchRandomChessGame(string gameDuration){
            int gameDurationParsered = int.Parse(gameDuration);
            Console.WriteLine(gameDurationParsered); 
            string userName = Context.User.Identity.Name;
            Console.WriteLine("--> Connection id: " + this.Context.ConnectionId + " and userId " + userName + " serching game");
            if(_chessGameService.CountOpponent(gameDurationParsered) == 0)
            {
                var playerInRoom = new PlayerInRoom(userName, gameDurationParsered, this.Context.ConnectionId);
                await _chessGameService.AddToWaitingList(playerInRoom);
                Console.WriteLine("--> Connected user witch connectionId " + userName + " was added to waiting player list");
                return "Added to waiting list";
            }
            else
            {
                //NA tym etapie, powinno być dołczenie do grup i zwrotka do obu graczy ze się zaczeło
                Console.WriteLine("--> Searching good opponent");
                var opponent = await _chessGameService.GetPlayerFromWaitingList(gameDurationParsered);
                Console.WriteLine("--> Connected user witch userId " + userName + " found player with id " + opponent.PlayerId + " " + opponent.ConnectionId);
                var room = Room.CreateRoom();
                Console.WriteLine("--> Create new room with id " + room.Id.ToString());

                Task t1 = Groups.AddToGroupAsync(this.Context.ConnectionId, room.Id.ToString());
                Task t2 = Groups.AddToGroupAsync(opponent.ConnectionId.ToString(), room.Id.ToString());
                
                t1.Wait();
                t2.Wait();
                
                await _chessGameService.RemoveFromWaitingList(opponent);

                await Clients.Client(this.Context.ConnectionId).SendAsync("GetColorPiece",ChessboardPieceColor.white.ToString());
                await  Clients.Client(opponent.ConnectionId).SendAsync("GetColorPiece",ChessboardPieceColor.black.ToString());
                                
                await Clients.Client(this.Context.ConnectionId).SendAsync("GetOpponent",opponent.PlayerName);
                await  Clients.Client(opponent.ConnectionId).SendAsync("GetOpponent",userName);
                                
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

        private enum ChessboardPieceColor{
            white,black
        }
    }
}