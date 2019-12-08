using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Settings;

namespace Chess.Infrastructure.Services
{
    public class TournamentService : ITournamentService
    {

        private readonly ITournamentRepository _tournamentRepository;
        private readonly IMapper _mapper;

        public TournamentService(ITournamentRepository tournamentRepository,IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
        }
        public async Task<TournamentDto> GetAsync(Guid tournamentId)
        {
            var tournament = await _tournamentRepository.GetAsync(tournamentId);
            if(tournament == null)
            {
                throw new Exception($"Tournament with id: '{tournamentId}' don't exists");
            }
            return _mapper.Map<Tournament,TournamentDto>(tournament);
        }
        public async Task<TournamentDto> GetAsync(string name)
        {
            var tournament = await _tournamentRepository.GetAsync(name);
            if(tournament == null)
            {
                throw new Exception($"Tournament with id: '{name}' don't exists");
            }
            return _mapper.Map<Tournament,TournamentDto>(tournament);
        }
        public async Task CreateAsync(string name, int maxPlayers)
        {
            var tournament = await _tournamentRepository.GetAsync(name);
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

    }
}