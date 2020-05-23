using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public class PlayerService: IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;   
        }

        public async Task<IEnumerable<PlayerDto>> GetAllAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Player>,IEnumerable<PlayerDto>>(players);

        }

        public async Task<PlayerDto> GetAsync(string email)
        {
            var player = await _playerRepository.GetAsync(email);
            if(player == null)
            {
                throw new Exception($"player with email: '{email}' was not found.");
            }
            return _mapper.Map<Player,PlayerDto>(player);
        }
    }
}