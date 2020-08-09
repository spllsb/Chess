using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Settings;
using Chess.Infrastructure.Extensions;
using System.Linq;

namespace Chess.Infrastructure.Services
{
    public class TournamentService : ITournamentService
    {

        private readonly ITournamentRepository _tournamentRepository;
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public TournamentService(ITournamentRepository tournamentRepository,
                                IPlayerService playerService,
                                IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _playerService = playerService;
            _mapper = mapper;
        }
        public async Task<TournamentDetailsDto> GetAsync(Guid tournamentId)
        {
            var tournament = await _tournamentRepository.GetAsync(tournamentId);
            if(tournament == null)
            {
                throw new Exception($"Tournament with id: '{tournamentId}' don't exists");
            }
            return _mapper.Map<Tournament,TournamentDetailsDto>(tournament);
        }
        public async Task<TournamentDetailsDto> GetAsync(string name)
        {
            var tournament = await _tournamentRepository.GetOrFailAsync(name);
            return _mapper.Map<Tournament,TournamentDetailsDto>(tournament);
        }
        public async Task CreateAsync(string name, int maxPlayers)
        {
            var tournament = await _tournamentRepository.GetTournamentAsync(name);
            if(tournament != null)
            {
                throw new Exception($"Tournament with name: '{tournament.Name}' already exists");
            }
            
            await _tournamentRepository.AddAsync(new Tournament(name,maxPlayers));
        }
        public async Task<IEnumerable<TournamentDto>> BrowseAsync()
        {
            var tournaments = await _tournamentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Tournament>,IEnumerable<TournamentDto>>(tournaments);
        }

        public async Task <IEnumerable<TournamentDto>> PagedList(TournamentParameters parameters)
        {
            var tournaments = _tournamentRepository.FindByCondition(x => true);
            SearchByName(ref tournaments, parameters.Name);

            var tournamentsAfterPagination =  tournaments
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();
            return _mapper.Map<IEnumerable<Tournament>,IEnumerable<TournamentDto>>(tournamentsAfterPagination);

        }
    
  

        private void SearchByName(ref IQueryable<Tournament> tournaments, string searchingName)
        {
            if (!tournaments.Any() || string.IsNullOrWhiteSpace(searchingName))
                return;

            tournaments = tournaments.Where(o => o.Name.ToLower().Contains(searchingName.Trim().ToLower()));
        }
    }

    public class TournamentParameters : QueryStringParameters
    {
        public string Name { get; set; }
    }
}