using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Domain.Enum;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Extensions;

namespace Chess.Infrastructure.Services
{
    public class ChessGameService : IChessGameService
    {
        static Random random = new Random();
        private List<string> pieceColorList = new List<string>(new string[] {ChessboardPieceColorEnum.white.ToString(),ChessboardPieceColorEnum.black.ToString()});
        private readonly IPlayerRepository _playerRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;
        private static ISet<PlayerInRoom> _waitingPlayerList = new HashSet<PlayerInRoom>();
        private static ISet<ChessMatch> _currentMatches = new HashSet<ChessMatch>();
        
        public ChessGameService(IPlayerRepository playerRepository,
                                IMatchRepository matchRepository,
                                IMapper mapper)
        {
                _playerRepository = playerRepository;
                _matchRepository = matchRepository;
                _mapper = mapper;
        }
        
        
        public async Task<int> GetWaitingPlayerListCount()
            => _waitingPlayerList.Count;

        public async Task AddToWaitingList(PlayerInRoom playerInRoom)
        {
            _waitingPlayerList.Add(playerInRoom);
            await Task.CompletedTask;
        }
        public int CountOpponent(int gameDuration, int ratingELO, SearchingGameTypEnum searchingGameTyp)
            => _waitingPlayerList.Where(x => x.GameDuration == gameDuration && x.SearchingGameTyp == searchingGameTyp).ToList().Count;
        

        public async Task RemoveCurrentPlayerFromWaitingList(string connectionId){
            var player = _waitingPlayerList.Where(x => x.ConnectionId == connectionId).FirstOrDefault();
            if(player != null)
            {
                await RemoveFromWaitingList(player);                
            }
        }
        public async Task<PlayerInRoom> GetPlayerFromWaitingList(int gameDuration, int ratingELO, SearchingGameTypEnum searchingGameTyp){
            var player = _waitingPlayerList.Where(x => x.GameDuration == gameDuration && x.SearchingGameTyp == searchingGameTyp).FirstOrDefault();
            if(player == null)
            {
                throw new Exception($"Any player was not found in waiting list");

            }
            return await Task.FromResult(player);
        }

        public async Task<PlayerInRoom> GetPlayerFromWaitingList(string connectionId){
            var player = _waitingPlayerList.Where(x => x.ConnectionId == connectionId).FirstOrDefault();
            return await Task.FromResult(player);
        }

        public async Task RemoveFromWaitingList(PlayerInRoom player)
        {
            _waitingPlayerList.Remove(player);
            await Task.CompletedTask;
        }

        public async Task InitMatch(PlayerInRoom playerOne, PlayerInRoom playerTwo, string roomId)
        {
            _currentMatches.Add(new ChessMatch(playerOne, playerTwo, roomId));
            await Task.CompletedTask;
        }



        public async Task<string> GetRoomId(string connectionId)
        {
            var game = _currentMatches.Where(x => x.PlayerOne.ConnectionId == connectionId || x.PlayerTwo.ConnectionId == connectionId).FirstOrDefault();
            if (game == null){
                throw new Exception("Connection id was not found in current matches");
            }
            return await Task.FromResult(game.RoomId);
        }

        public async Task RemoveMatch(string roomId)
        => _currentMatches.Remove(await GetChessMatch(roomId));

        public async Task<ChessMatch> GetChessMatch (string roomId)
        => await Task.FromResult(_currentMatches.Where(x => x.RoomId == roomId).FirstOrDefault());


        public async Task SaveChessMatchOnDatabase(string roomId)
        {
            var chessMatch = await GetChessMatch(roomId);
            await _matchRepository.AddAsync(chessMatch.Match);
        }

        public async Task<string> GetRandomColorPiece()
        {
            int r = random.Next(pieceColorList.Count);
            return pieceColorList[r];
        }
    }





    public class PlayerInRoom : Player{
        public int GameDuration { get; set; }
        public string ConnectionId { get; set; }
        public SearchingGameTypEnum SearchingGameTyp { get; set; }
        public PlayerInRoom(Guid playerId,string playerName, int gameDuration, string connectionId, SearchingGameTypEnum searchingGameTyp)
        {
            UserId = playerId;
            Username = playerName;
            GameDuration = gameDuration;
            ConnectionId = connectionId;
            SearchingGameTyp = searchingGameTyp;
        }

        public PlayerInRoom(PlayerDto player, int gameDuration, string connectionId, SearchingGameTypEnum searchingGameTyp)
        {
            UserId = player.PlayerId;
            Username = player.Username;
            RatingElo = player.RatingElo;
            GameDuration = gameDuration;
            ConnectionId = connectionId;
            SearchingGameTyp = searchingGameTyp;
        }
    }   

    public class ChessMatch{
        public PlayerInRoom PlayerOne { get; set; }
        public PlayerInRoom PlayerTwo { get; set; }
        public Match Match { get; set; }
        public string RoomId { get; protected set; }  
        public string PGN {get;set;}
        public bool isSaved {get;set;} = false;

        public ChessMatch(PlayerInRoom player1, PlayerInRoom player2, string roomId)
        {
            PlayerOne = player1;
            PlayerTwo = player2;
            RoomId = roomId;
            Match = Match.Create(player1.UserId, player2.UserId);
        }
    }
}


