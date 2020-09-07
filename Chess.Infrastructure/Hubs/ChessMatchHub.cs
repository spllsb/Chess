using System;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Core.Domain.Enum;
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
        private readonly IELOProvider _eloProvider;
        private readonly IPlayerService _playerService;

        public ChessMatchHub(IChessGameService chessGameService,
                            IOptions<ChessGameSettings> chessGameSettings,
                            IUserService userService,
                            IFileProvider fileProvider,
                            IELOProvider eLOProvider,
                            IPlayerService playerService
                            )
        {
            _chessGameService = chessGameService;
            _chessGameSettings = chessGameSettings.Value;
            _userService = userService;
            _fileProvider = fileProvider;
            _eloProvider = eLOProvider;
            _playerService = playerService;
        }

        public override async Task OnConnectedAsync() 
        {
            Console.WriteLine("--> Connection Established " + Context.ConnectionId);
            // await Clients.Client(this.Context.ConnectionId).SendAsync("ReceiveConnID", this.Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            if (await _chessGameService.GetPlayerFromWaitingList(this.Context.ConnectionId) != null){
                //If player was in waiting list : Remove from waiting list
                await _chessGameService.RemoveCurrentPlayerFromWaitingList(this.Context.ConnectionId);
            }
            else{
                //If player was in play: Call opponent about disconnect 
                var roomId = await _chessGameService.GetRoomId(this.Context.ConnectionId);
                await Clients.Group(roomId).SendAsync("ReceiveCommunication", "Opponent left the game");
                var chessMatch = await _chessGameService.GetChessMatch(roomId);
                if(!chessMatch.isSaved)
                {
                    //create pgn file 
                    var fileName = GenerateFileName("aa", "bb","pgn");
                    // chessMatch.Match.PgnPath = fileName;
                    chessMatch.Match.PgnFileName = fileName;
                    await _fileProvider.SaveFile(_chessGameSettings.PGNFilePath, fileName, chessMatch.PGN);
                    await _chessGameService.SaveChessMatchOnDatabase(roomId);
                    chessMatch.isSaved = true;
                }
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

public async Task<string> SearchRandomChessGame(int gameDuration, 
                                                string playerId){
    float RaW = 0, RaD = 0, RaL = 0, RbW = 0, RbD = 0, RbL = 0;
    var currentPlayer = await _playerService
                        .GetAsync(Context.User.Identity.Name);
    var currentPlayerInRoom = new PlayerInRoom(currentPlayer, 
                                             gameDuration,
                                             this.Context.ConnectionId, 
                                             SearchingGameTypEnum.random);
    Console.WriteLine("--> Player " + currentPlayer.Username +
                    "was added to room");
    if(_chessGameService.CountOpponent(gameDuration, 
                                    currentPlayer.RatingElo, 
                                    SearchingGameTypEnum.random) == 0){
        await _chessGameService.AddToWaitingList(currentPlayerInRoom);
        Console.WriteLine("--> " + currentPlayer.Username +
                        "was added to waiting list");
        return "Added to waiting list";
    }
    else{
        var opponent = await _chessGameService
            .GetPlayerFromWaitingList(gameDuration, 
                                    currentPlayer.RatingElo, 
                                    SearchingGameTypEnum.random);
        Console.WriteLine("--> " + currentPlayer.Username + 
                        "found opponent " + opponent.Username);
        await _chessGameService
            .RemoveFromWaitingList(opponent);
        Console.WriteLine("--> Player " + opponent.Username +
                        " was removed from waiting list"); 
        var room = Room.CreateRoom();
        Console.WriteLine("--> Create new room with id " + 
                          room.Id.ToString());
        Task t1 = Groups.AddToGroupAsync(this.Context.ConnectionId, 
                                        room.Id.ToString());
        Task t2= Groups.AddToGroupAsync(opponent.ConnectionId.ToString(), 
                                        room.Id.ToString());
        await _chessGameService.InitMatch(currentPlayerInRoom, opponent, 
                                        room.Id.ToString());
        t1.Wait();
        t2.Wait();
        _eloProvider.CalcELORating(currentPlayer.RatingElo, 
                            opponent.RatingElo,ChessGameResultEnum.WIN, 
                            ref RaW, ref RbL);
        _eloProvider.CalcELORating(currentPlayer.RatingElo, 
                            opponent.RatingElo,ChessGameResultEnum.DRAW, 
                            ref RaD, ref RbD);
        _eloProvider.CalcELORating(currentPlayer.RatingElo, 
                            opponent.RatingElo,ChessGameResultEnum.LOSE, 
                            ref RaL, ref RbW);
        await Clients.Client(this.Context.ConnectionId)
                            .SendAsync("GetNewRatingELO",RaW,RaD,RaL);
        await Clients.Client(opponent.ConnectionId)
                            .SendAsync("GetNewRatingELO",RbW,RbD,RbL);
        var currentPieceColor = await _chessGameService
                                    .GetRandomColorPiece();
        var opponentPieceColor = currentPieceColor
                                .Equals(ChessboardPieceColorEnum.white.ToString()) ?
                                ChessboardPieceColorEnum.black.ToString() :
                                ChessboardPieceColorEnum.white.ToString(); 
        await Clients.Client(this.Context.ConnectionId)
                            .SendAsync("GetColorPiece", 
                                    currentPieceColor);
        await  Clients.Client(opponent.ConnectionId)
                      .SendAsync("GetColorPiece", 
                      opponentPieceColor);
        await Clients.Client(this.Context.ConnectionId)
                     .SendAsync("GetOpponentInformation", 
                        opponent.Username, 
                        opponent.RatingElo);
        await  Clients.Client(opponent.ConnectionId)
                      .SendAsync("GetOpponentInformation", 
                                currentPlayer.Username, 
                                currentPlayer.RatingElo);
        await Clients.Group(room.Id.ToString())
                     .SendAsync("ReceiveRoom",
                                room.Id.ToString());
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