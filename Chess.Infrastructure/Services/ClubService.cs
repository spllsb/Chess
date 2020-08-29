using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Repositories;

namespace Chess.Infrastructure.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;


        public ClubService( IClubRepository clubRepository,
                            IMapper mapper)
        {
            _clubRepository = clubRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClubDto>> GetAllClubs()
        {
            var clubs = await _clubRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Club>,IEnumerable<ClubDto>>(clubs);
        }

        public async Task<ClubDto> GetClub(string name)
        {
            var club = await _clubRepository.GetAsync(name);
            return _mapper.Map<Club, ClubDto>(club);
        }
    }
}