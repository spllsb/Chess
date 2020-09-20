using System;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Core.Domain.Enum;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Chess.Infrastructure.Hubs
{
    public class ChessMatchHub : Hub
    {
        private readonly IChessGameService _chessGameService;
        private readonly ChessGameSettings _chessGameSettings;
        private readonly IFileProvider _fileProvider;
        private readonly IELOProvider _eloProvider;
        private readonly IPlayerService _playerService;
        private readonly ILogger _logger;
        private readonly IMatchService _matchService;


        public ChessMatchHub(IChessGameService chessGameService,
                            IOptions<ChessGameSettings> chessGameSettings,
                            IFileProvider fileProvider,
                            IELOProvider eLOProvider,
                            IPlayerService playerService,
                            ILoggerFactory loggerFactory,
                            IMatchService matchService
                            )
        {
            _chessGameService = chessGameService;
            _chessGameSettings = chessGameSettings.Value;
            _fileProvider = fileProvider;
            _eloProvider = eLOProvider;
            _playerService = playerService;
            _matchService = matchService;
            _logger = loggerFactory.CreateLogger<ChessMatchHub>();
        }

        public override async Task OnConnectedAsync() 
        {
            _logger.LogInformation(3, "--> Connection Established " + Context.ConnectionId);           
            // await Clients.Client(this.Context.ConnectionId).SendAsync("ReceiveConnID", this.Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var player = await _chessGameService.GetCurrentPlayer(this.Context.ConnectionId);
            switch(player.Status){
                case PlayerStatusEnum.inRoom:
                    await Clients.Group(player.ActualRoomId).SendAsync("ReceiveCommunication",  @Context.User.Identity.Name + " left the game");
                    _logger.LogInformation(3, $"{player.UserName} was removed from room");

                break;
                case PlayerStatusEnum.inWaitingList:
                    await _chessGameService.RemoveCurrentPlayerFromWaitingList(this.Context.ConnectionId);
                    _logger.LogInformation(3, $"{player.UserName} was removed from waiting list player");
                break;
                case PlayerStatusEnum.inGame:
                    await Clients.Group(player.ActualRoomId).SendAsync("ReceiveCommunication",  @Context.User.Identity.Name + " left the game");
                    var chessMatch = await _chessGameService.GetChessMatch(player.ActualRoomId);
                    if(chessMatch != null)
                    {
                        var fileName = GenerateFileName("aa", "bb","pgn");
                        chessMatch.Match.PgnFileName = fileName;
                        await _fileProvider.SaveFile(_chessGameSettings.PGNFilePath, fileName, chessMatch.PGN);
                        await _chessGameService.SaveChessMatchOnDatabase(player.ActualRoomId);
                        chessMatch.isSaved = true;
                        await _chessGameService.RemoveMatch(player.ActualRoomId);
                    }
                break;
            }
            await _chessGameService.RemoveCurrentPlayer(player);

            _logger.LogInformation(3, $"--> This moment is {await _chessGameService.GetCurrentMatchCount()} current games.");
            _logger.LogInformation(3, $"--> Now is {await _chessGameService.GetCurrentPlayerListCount()} players in current player list");
            _logger.LogInformation(3, $"--> Now is {await _chessGameService.GetWaitingPlayerListCount()} players in waiting list");
            _logger.LogInformation(3, $"--> [{this.Context.ConnectionId}] is closed");
            await base.OnDisconnectedAsync(exception);
        }



        public async Task SendPosition(string chessGameId, MoveObject move)
        {
            await Clients.Group(chessGameId).SendAsync("ReceivePosition", 
                                                    chessGameId, 
                                                    Context.User.Identity.Name,
                                                    move);
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

        public async Task Test(){
            float RaW = 0, RaD = 0, RaL = 0, RbW = 0, RbD = 0, RbL = 0;
            //pobierz wszystkie mecze któe mają sięzacząć teraz 

            var chessMatchList = await _matchService.GetMatchStartingNowAsync();
            _logger.LogInformation(3, $"Found {chessMatchList.Count()} matches to created in Handfire schedule process");
            foreach (var chessMatch in chessMatchList)
            {
                var player1 = await _chessGameService.GetCurrentPlayer(chessMatch.FirstPlayerId);
                var player2 = await _chessGameService.GetCurrentPlayer(chessMatch.SecondPlayerId);
                if(player1==null || player2 == null){
                    var player = player1 != null ? player1 : player2;
                    await  Clients.Client(player.ConnectionId)
                                .SendAsync("ReceiveCommunication","Wygrana, bo sie przeciwnik nie pojawił");
                }
    
                if(player1!=null && player2!=null)
                {
                    await _chessGameService.InitMatch(player1, player2, 
                            chessMatch.Id.ToString());
                            
                    player1.Status = PlayerStatusEnum.inGame;
                    player1.ActualRoomId = chessMatch.Id.ToString();
                    player2.Status = PlayerStatusEnum.inGame;
                    player2.ActualRoomId = chessMatch.Id.ToString();

                    _eloProvider.CalcELORating(player1.RatingElo, 
                                        player2.RatingElo,ChessGameResultEnum.WIN, 
                                        ref RaW, ref RbL);
                    _eloProvider.CalcELORating(player1.RatingElo, 
                                        player2.RatingElo,ChessGameResultEnum.DRAW, 
                                        ref RaD, ref RbD);
                    _eloProvider.CalcELORating(player1.RatingElo, 
                                        player2.RatingElo,ChessGameResultEnum.LOSE, 
                                        ref RaL, ref RbW);

                    var currentPieceColor = await _chessGameService
                                                 .GetRandomColorPiece();
                    var opponentPieceColor = currentPieceColor
                                            .Equals(ChessboardPieceColorEnum.white.ToString()) ?
                                            ChessboardPieceColorEnum.black.ToString() :
                                            ChessboardPieceColorEnum.white.ToString();                                        
                    await Clients.Client(player1.ConnectionId)
                                .SendAsync("GetOpponentInformation", 
                                        player2.UserName, 
                                        player2.RatingElo,
                                        player2.AvatarImageName,
                                        opponentPieceColor);
                    await  Clients.Client(player2.ConnectionId)
                                .SendAsync("GetOpponentInformation", 
                                        player1.UserName, 
                                        player1.RatingElo,
                                        player1.AvatarImageName,
                                        currentPieceColor);
                    await Clients.Client(player1.ConnectionId)
                                        .SendAsync("GetColorPiece", 
                                                currentPieceColor);
                    await  Clients.Client(player2.ConnectionId)
                                .SendAsync("GetColorPiece", 
                                opponentPieceColor);

                    await Clients.Client(player1.ConnectionId)
                                        .SendAsync("GetNewRatingELO",RaW,RaD,RaL);
                    await Clients.Client(player2.ConnectionId)
                                        .SendAsync("GetNewRatingELO",RbW,RbD,RbL);

                    await Clients.Group(chessMatch.Id.ToString())
                                .SendAsync("InitializeChessGame");
                }
            }
        }

        private string GenerateFileName(string player1, string player2, string extension)
        => $"{player1}_vs_{player2}_{DateTime.Now.ToString("yyyyddMMHHmmss")}.{extension}";

        // public async Task ChoosePieceColor(string )
        public string GetConnectionId(){
            return this.Context.ConnectionId;
        }




//stworzyć fnkcję która wywoływania jest z pozionu javascript o pewnej godzienie
//ma inicjsować mecz 

public async Task AddUserToParticularChessGameRoom(string roomId){
    var currentPlayer = await _playerService
                            .GetAsync(Context.User.Identity.Name);
    var chessMatch = await _matchService.GetAsync(Guid.Parse(roomId));
    var playerInRoom = new PlayerInRoom(currentPlayer, 
                                            0,
                                            this.Context.ConnectionId, 
                                            SearchingGameTypEnum.particular,
                                            PlayerStatusEnum.inRoom,
                                            roomId);
    await _chessGameService.AddPlayerToCurrentPlayerList(playerInRoom);

    _logger.LogInformation(3,"--> Player " + currentPlayer.UserName +
                        " was added to room " +
                        roomId);
    await this.Groups.AddToGroupAsync(this.Context.ConnectionId, roomId);

    await Clients.Group(roomId).SendAsync("ReceiveCommunication", currentPlayer.UserName + " dołączył do pokoju");
}







public async Task InitChessGame(int gameDuration){
    System.Console.WriteLine(gameDuration);
    float RaW = 0, RaD = 0, RaL = 0, RbW = 0, RbD = 0, RbL = 0;
    var currentPlayer = await _playerService
                        .GetAsync(Context.User.Identity.Name);
    var playerInRoom = new PlayerInRoom(currentPlayer, 
                                            gameDuration,
                                            this.Context.ConnectionId, 
                                            SearchingGameTypEnum.random,
                                            PlayerStatusEnum.inRoom,
                                            null);
    await _chessGameService.AddPlayerToCurrentPlayerList(playerInRoom);
                                            
    _logger.LogInformation(3,"--> Player " + currentPlayer.UserName +
                    " was added to room");
    if(_chessGameService.CountOpponent(gameDuration, 
                                    currentPlayer.RatingElo, 
                                    SearchingGameTypEnum.random) == 0){
        await _chessGameService.AddToWaitingList(playerInRoom);
        playerInRoom.Status = PlayerStatusEnum.inWaitingList;
        _logger.LogInformation(3,"--> " + currentPlayer.UserName +
                        " was added to waiting list");
        await Clients.Caller.SendAsync("ReceiveCommunication", 
                                    "Szukam gry...");


        // return "Added to waiting list";
    }
    else{
        var opponent = await _chessGameService
            .GetPlayerFromWaitingList(gameDuration, 
                                    currentPlayer.RatingElo, 
                                    SearchingGameTypEnum.random);
        _logger.LogInformation(3,"--> " + currentPlayer.UserName + 
                        " found opponent " + opponent.UserName);
        await _chessGameService
            .RemoveFromWaitingList(opponent);
        
        _logger.LogInformation(3,"--> Player " + opponent.UserName +
                        " was removed from waiting list"); 
        var room = Room.CreateRoom();
        _logger.LogInformation(3,"--> Create new room with id " + 
                          room.Id.ToString());
        Task t1 = Groups.AddToGroupAsync(this.Context.ConnectionId, 
                                        room.Id.ToString());
        Task t2= Groups.AddToGroupAsync(opponent.ConnectionId.ToString(), 
                                        room.Id.ToString());
        await _chessGameService.InitMatch(playerInRoom, opponent, 
                                        room.Id.ToString());
        playerInRoom.Status = PlayerStatusEnum.inGame;
        playerInRoom.ActualRoomId = room.Id.ToString();
        opponent.Status = PlayerStatusEnum.inGame;
        opponent.ActualRoomId = room.Id.ToString();

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
        var currentPieceColor = await _chessGameService
                                    .GetRandomColorPiece();
        var opponentPieceColor = currentPieceColor
                                .Equals(ChessboardPieceColorEnum.white.ToString()) ?
                                ChessboardPieceColorEnum.black.ToString() :
                                ChessboardPieceColorEnum.white.ToString(); 

        await Clients.Client(this.Context.ConnectionId)
                     .SendAsync("GetOpponentInformation", 
                        opponent.UserName, 
                        opponent.RatingElo,
                        opponent.AvatarImageName,
                        opponentPieceColor);
        await  Clients.Client(opponent.ConnectionId)
                      .SendAsync("GetOpponentInformation", 
                                currentPlayer.UserName, 
                                currentPlayer.RatingElo,
                                currentPlayer.AvatarImageName,
                                currentPieceColor);
        await Clients.Client(this.Context.ConnectionId)
                            .SendAsync("GetColorPiece", 
                                    currentPieceColor);
        await  Clients.Client(opponent.ConnectionId)
                      .SendAsync("GetColorPiece", 
                      opponentPieceColor);

        await Clients.Client(this.Context.ConnectionId)
                            .SendAsync("GetNewRatingELO",RaW,RaD,RaL);
        await Clients.Client(opponent.ConnectionId)
                            .SendAsync("GetNewRatingELO",RbW,RbD,RbL);

        await Clients.Group(room.Id.ToString())
                    .SendAsync("InitializeChessGame");

        await Clients.Group(room.Id.ToString())
                     .SendAsync("ReceiveRoom",
                                room.Id.ToString());
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