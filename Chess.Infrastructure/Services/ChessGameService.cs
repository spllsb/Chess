using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Extensions;

namespace Chess.Infrastructure.Services
{
    public class ChessGameService : IChessGameService
    {
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
        
        
        public async Task AddToWaitingList(PlayerInRoom playerInRoom)
        {
            _waitingPlayerList.Add(playerInRoom);
        }

        public int CountOpponent(int gameDuration)
            => _waitingPlayerList.Where(x => x.GameDuration == gameDuration).ToList().Count;
        

        public async Task RemoveCurrentPlayerFromWaitingList(string connectionId){
            var player = _waitingPlayerList.Where(x => x.ConnectionId == connectionId).FirstOrDefault();
            if(player != null)
            {
                await RemoveFromWaitingList(player);                
            }
        }
        public async Task<PlayerInRoom> GetPlayerFromWaitingList(int gameDuration){
            var player = _waitingPlayerList.Where(x => x.GameDuration == gameDuration).FirstOrDefault();
            if(player == null)
            {
                throw new Exception($"Any player was not found in waiting list");

            }
            return player;
        }

        public async Task RemoveFromWaitingList(PlayerInRoom player)
        {
            _waitingPlayerList.Remove(player);
        }

        public async Task InitMatch(PlayerInRoom playerOne, PlayerInRoom playerTwo, string roomId)
        {
            _currentMatches.Add(new ChessMatch(playerOne, playerTwo, roomId));
        }



        public async Task<string> GetRoomId(string connectionId)
        {
            var game = _currentMatches.Where(x => x.PlayerOne.ConnectionId == connectionId || x.PlayerTwo.ConnectionId == connectionId).FirstOrDefault();
            return game.RoomId;
        }

        public async Task RemoveMatch(string roomId)
        {

        }

        public async Task<ChessMatch> GetChessMatch (string roomId)
        {
            return _currentMatches.Where(x => x.RoomId == roomId).FirstOrDefault();
        }


        public async Task SaveChessMatchOnDatabase(string roomId)
        {
            var chessMatch = await GetChessMatch(roomId);
            Console.WriteLine("aaaaaaaaaaaaaaaaaa " + chessMatch.PlayerOne.PlayerId);
            var match = Match.Create(new Guid("0d78f06a-c8ea-4495-828d-6a2bae99d6b4"), new Guid("c5b84954-d711-40fe-8bb2-8620b7e4b8be"));
            await _matchRepository.AddAsync(match);
        }
    }


    public class PlayerInRoom{
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int GameDuration { get; set; }
        public string ConnectionId { get; set; }
        public PlayerInRoom(string playerId,string playerName, int gameDuration, string connectionId)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            GameDuration = gameDuration;
            ConnectionId = connectionId;
        }
    }   

    public class ChessMatch{
        public PlayerInRoom PlayerOne { get; set; }
        public PlayerInRoom PlayerTwo { get; set; }
        public string RoomId { get; protected set; }  
        public string PGN {get; set;}
        public string FEN {get;set;}
        public bool isSaved {get;set;} = false;

        public ChessMatch(PlayerInRoom playerOne, PlayerInRoom playerTwo, string roomId)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            RoomId = roomId;
        }
    }
}


