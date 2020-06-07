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
        
        
        public async Task AddToWaitingList(string userId, string connectionId)
        {
            var player = new PlayerInRoom(userId, connectionId);
            _waitingPlayerList.Add(player);
        }

        public int CountOpponent()
            => _waitingPlayerList.Count;
        

        public async Task<PlayerInRoom> GetPlayerFromWaitingList(){
            var player = _waitingPlayerList.SingleOrDefault();
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
        public string ConnectionId { get; set; }
        public PlayerInRoom(string playerId, string connectionId)
        {
            PlayerId = playerId;
            ConnectionId = connectionId;
        }
    }   

}


