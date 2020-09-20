using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<MatchDto>> GetByTournamentAsync(Guid tournamentId)
        {
            var matches = await _matchRepository.GetMatchByTournamentAsync(tournamentId);
            return _mapper.Map<IEnumerable<Match>,IEnumerable<MatchDto>>(matches);
        }

        public async Task<IEnumerable<MatchDto>> GetByTournamentRoundAsync(Guid tournamentId)
        {
            var matches = await _matchRepository.GetMatchByTournamentAsync(tournamentId);
            return _mapper.Map<IEnumerable<Match>,IEnumerable<MatchDto>>(matches);
        }

        public async Task<IEnumerable<MatchDto>> GetMatchStartingNowAsync()
        {
            DateTime NowTime = DateTime.Now;
            DateTime MyDate = new DateTime(NowTime.Year, NowTime.Month, NowTime.Day,NowTime.Hour,NowTime.Minute,0);
            var matches = await _matchRepository.FindByCondition(x => x.BeginAt == MyDate).ToListAsync();
            return _mapper.Map<IEnumerable<Match>,IEnumerable<MatchDto>>(matches);
        }

        public async Task CreateMatch(Guid firstPlayerId, Guid secondPlayerId, DateTime begin_at, int duringTime)
        {
            await _matchRepository.AddAsync(Match.Create(firstPlayerId, secondPlayerId, begin_at, duringTime));
        }


        public async Task <IEnumerable<MatchDto>> PagedList(MatchParameters parameters)
        {
            var matches = _matchRepository.FindByCondition(x => x.TournamentId == parameters.TournamentId && x.Round == parameters.Round);

            var matchesAfterPagination =  matches
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();
            return _mapper.Map<IEnumerable<Match>,IEnumerable<MatchDto>>(matchesAfterPagination);
        }
    }


    public class MatchParameters : QueryStringParameters
    {
        public Guid TournamentId { get; set; }
        public int Round { get; set; }
    }
}