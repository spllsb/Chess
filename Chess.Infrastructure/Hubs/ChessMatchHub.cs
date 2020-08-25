using System;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Chess.Infrastructure.Hubs
{
    public class ChessMatchHub : Hub
    {
        private readonly IChessGameService _chessGameService;
        private readonly ChessGameSettings _chessGameSettings;
        private readonly IFileProvider _fileProvider;
        private readonly IUserService _userService;

        public ChessMatchHub(IChessGameService chessGameService,
                            IOptions<ChessGameSettings> chessGameSettings,
                            IUserService userService,
                            IFileProvider fileProvider
                            )
        {
            _chessGameService = chessGameService;
            _chessGameSettings = chessGameSettings.Value;
            _userService = userService;
            _fileProvider = fileProvider;
        }

        public override async Task OnConnectedAsync() 
        {
            Console.WriteLine("--> Connection Established " + Context.ConnectionId);
            // await Clients.Client(this.Context.ConnectionId).SendAsync("ReceiveConnID", this.Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //If player was in waiting list : Remove from waiting list
            await _chessGameService.RemoveCurrentPlayerFromWaitingList(this.Context.ConnectionId);
            //If player was in play: Call opponent about disconnect 
            var roomId = await _chessGameService.GetRoomId(this.Context.ConnectionId);
            await Clients.Group(roomId).SendAsync("ReceiveCommunication", "Opponent left the game");

            var chessMatch = await _chessGameService.GetChessMatch(roomId);
            
            if(!chessMatch.isSaved)
            {
                //create pgn file 
                //save game to database 
                var fileName = GenerateFileName("aa", "bb","pgn");
                // chessMatch.Match.PgnPath = fileName;
                chessMatch.Match.PgnFileName = fileName;
                await _fileProvider.SaveFile(_chessGameSettings.PGNFilePath, fileName, chessMatch.PGN);
                await _chessGameService.SaveChessMatchOnDatabase(roomId);
                chessMatch.isSaved = true;
            }

            Console.WriteLine($"[{this.Context.ConnectionId}] is closed");
            Console.WriteLine(exception);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendPosition(string chessGameId, MoveObject move)
        {
            await Clients.Group(chessGameId).SendAsync("ReceivePosition", chessGameId, Context.User.Identity.Name, move);
        }

        public async Task SaveInformationAboutGame(string chessGameId, string pgn, string fen)
        {
                var chessMatch = await _chessGameService.GetChessMatch(chessGameId);
                chessMatch.PGN = pgn;
                chessMatch.Match.Fen = fen;
        }
        public async Task SendCommunication(string communication, string groupId)
        {
            await Clients.Group(groupId).SendAsync("ReceiveCommunication", communication);
        }



        private string GenerateFileName(string player1, string player2, string extension)
        => $"{player1}_vs_{player2}_{DateTime.Now.ToString("yyyyddMMHHmmss")}.{extension}";

        // public async Task ChoosePieceColor(string )
        public string GetConnectionId(){
            return this.Context.ConnectionId;
        }

        public async Task<string> SearchRandomChessGame(string gameDuration){
            int gameDurationParsered = int.Parse(gameDuration);
            Console.WriteLine(gameDurationParsered); 
            string userName = Context.User.Identity.Name;
            Console.WriteLine("--> Connection id: " + this.Context.ConnectionId + " and userId " + userName + " serching game");

            var currentPlayer = new PlayerInRoom(new Guid("c843d008-8610-4959-9b80-9569ff02ddc0"), userName, gameDurationParsered, this.Context.ConnectionId);
            if(_chessGameService.CountOpponent(gameDurationParsered) == 0)
            {
                await _chessGameService.AddToWaitingList(currentPlayer);
                Console.WriteLine("--> Connected user witch connectionId " + userName + " was added to waiting player list");
                return "Added to waiting list";
            }
            else
            {
                //NA tym etapie, powinno być dołczenie do grup i zwrotka do obu graczy ze się zaczeło
                Console.WriteLine("--> Searching good opponent");
                var opponent = await _chessGameService.GetPlayerFromWaitingList(gameDurationParsered);
                Console.WriteLine("--> Connected user witch userId " + userName + " found player with id " + opponent.UserId + " " + opponent.ConnectionId);
                var room = Room.CreateRoom();
                Console.WriteLine("--> Create new room with id " + room.Id.ToString());

                Task t1 = Groups.AddToGroupAsync(this.Context.ConnectionId, room.Id.ToString());
                Task t2 = Groups.AddToGroupAsync(opponent.ConnectionId.ToString(), room.Id.ToString());

                await _chessGameService.InitMatch(currentPlayer, opponent, room.Id.ToString());

                t1.Wait();
                t2.Wait();
                
                await _chessGameService.RemoveFromWaitingList(opponent);

                await Clients.Client(this.Context.ConnectionId).SendAsync("GetColorPiece",ChessboardPieceColor.white.ToString());
                await  Clients.Client(opponent.ConnectionId).SendAsync("GetColorPiece",ChessboardPieceColor.black.ToString());
                                
                await Clients.Client(this.Context.ConnectionId).SendAsync("GetOpponent",opponent.Username);
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