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
        private readonly IMapper _mapper;
        private static ISet<PlayerInRoom> _waitingPlayerList = new HashSet<PlayerInRoom>();
        private static ISet<PlayerInGame> _currentMatches = new HashSet<PlayerInGame>();
        
        public ChessGameService(IPlayerRepository playerRepository,
                                IMapper mapper)
        {
                _playerRepository = playerRepository;
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
            _currentMatches.Add(new PlayerInGame(playerOne, playerTwo, roomId));
        }
        public async Task<string> GetRoomId(string connectionId)
        {
            var game = _currentMatches.Where(x => x.PlayerOne.ConnectionId == connectionId || x.PlayerTwo.ConnectionId == connectionId).FirstOrDefault();
            return game.RoomId;
        }
    }


    public class PlayerInRoom{
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int GameDuration { get; set; }
        public string ConnectionId { get; set; }
        public PlayerInRoom(string playerName, int gameDuration, string connectionId)
        {
            PlayerName = playerName;
            GameDuration = gameDuration;
            ConnectionId = connectionId;
        }
    }   

    public class PlayerInGame{
        public PlayerInRoom PlayerOne { get; set; }
        public PlayerInRoom PlayerTwo { get; set; }
        public string RoomId { get; protected set; }  

        public PlayerInGame(PlayerInRoom playerOne, PlayerInRoom playerTwo, string roomId)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            RoomId = roomId;
        }
    }
}


