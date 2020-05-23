using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public class MatchService : IMatchService
    {

        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;   
        }
        public async Task<MatchDto> GetAsync(Guid matchId)
        {
            var match = await _matchRepository.GetAsync(matchId);
            if(match == null)
            {
                throw new Exception($"Match with matchId: '{matchId}' was not found.");
            }
            return _mapper.Map<Match,MatchDto>(match);
        }
        public async Task<IEnumerable<MatchDto>> BrowseAsync()
        {
            var matches = await _matchRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Match>,IEnumerable<MatchDto>>(matches);
        }

        public async Task<IEnumerable<MatchDto>> GetByPlayerAsync(Guid playerId)
        {
            var matches = await _matchRepository.GetMatchByPlayerAsync(playerId);
            return _mapper.Map<IEnumerable<Match>,IEnumerable<MatchDto>>(matches);
        }
    }
}