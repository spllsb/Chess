using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository playerRepository, IMatchRepository matchRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _matchRepository = matchRepository;
            _mapper = mapper;   
        }

        public async Task<IEnumerable<PlayerDto>> GetAllAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Player>,IEnumerable<PlayerDto>>(players);

        }

        public Task<IEnumerable<PlayerDetailsDto>> GetPlayerMatches(Guid playerId)
        {
            var player = _playerRepository.GetAsync(playerId);
            var matches = _matchRepository.GetMatchByPlayerAsync(playerId);
            throw new NotImplementedException();
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

        public async Task <IEnumerable<PlayerDto>> PagedList(PlayerParameters parameters)
        {
            var players = _playerRepository.FindByCondition(x => true);
            SearchByName(ref players, parameters.Name);

            var playersAfterPagination =  players
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();
            return _mapper.Map<IEnumerable<Player>,IEnumerable<PlayerDto>>(playersAfterPagination);
        }

        private void SearchByName(ref IQueryable<Player> players, string searchingName)
        {
            if (!players.Any() || string.IsNullOrWhiteSpace(searchingName))
                return;

            players = players.Where(o => o.Username.ToLower().Contains(searchingName.Trim().ToLower()));
        }
    }
    public class PlayerParameters : QueryStringParameters
    {
        // public new int PageSize = 2;
        public string Name { get; set; }
    }



}