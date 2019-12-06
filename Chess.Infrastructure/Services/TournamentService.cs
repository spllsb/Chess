using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;

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
            await _tournamentRepository.AddAsync(new Tournament(name,maxPlayers));
        }
        public Task<IEnumerable<TournamentDetailsDto>> BrowseAsync()
        {
            throw new NotImplementedException();
        }

    }
}