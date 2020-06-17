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

}


