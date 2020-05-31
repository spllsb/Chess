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
        private static ISet<Player> _waitingPlayerList = new HashSet<Player>();

        
        public ChessGameService(IPlayerRepository playerRepository,
                                IMapper mapper)
        {
                _playerRepository = playerRepository;
                _mapper = mapper;
        }
        
        
        public async Task AddToWaitingList(string userId)
        {
            var player = await _playerRepository.GetOrFailAsync(Guid.Parse(userId));
            _waitingPlayerList.Add(player);
        }

        public async Task<PlayerDto> GetPlayerFromWaitingList(){
            var player = _waitingPlayerList.FirstOrDefault();
            if(player == null)
            {
                new Exception($"Any player was not found in waiting list");
                return null;
            }
            return _mapper.Map<Player,PlayerDto>(player);
        }
    }
}