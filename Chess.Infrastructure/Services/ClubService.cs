using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateAsync(string name, string contactEmail, string pictureName, string information)
        {
            await _clubRepository.AddAsync(new Club(name,contactEmail,pictureName,information));
        }

        public async Task<IEnumerable<ClubDto>> GetAllClubs()
        {
            var clubs = await _clubRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Club>,IEnumerable<ClubDto>>(clubs);
        }

        public async Task<ClubDetailsDto> GetAsync(Guid clubId)
        {
            var club = await _clubRepository.GetAsync(clubId);
            return _mapper.Map<Club, ClubDetailsDto>(club);
        }

        public async Task<ClubDetailsDto> GetAsync(string name)
        {
            var club = await _clubRepository.GetAsync(name);
            return _mapper.Map<Club, ClubDetailsDto>(club);
        }

        public async Task <IEnumerable<ClubDto>> PagedList(ClubParameters parameters)
        {
            var clubs = _clubRepository.FindByCondition(x => true);
            SearchByName(ref clubs, parameters.Name);

            var clubsAfterPagination =  clubs
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();
            return _mapper.Map<IEnumerable<Club>,IEnumerable<ClubDto>>(clubsAfterPagination);
        }

        public async Task UpdateAsync(ClubDetailsDto clubDetailsDto)
        {
            var club = await _clubRepository.GetAsync(clubDetailsDto.Name);
            club.Upadate(club.Name,club.ContactEmail,club.PictureName, club.Information);
            await _clubRepository.UpdateAsync(club);
        }

        private void SearchByName(ref IQueryable<Club> clubs, string searchingName)
        {
            if (!clubs.Any() || string.IsNullOrWhiteSpace(searchingName))
                return;

            clubs = clubs.Where(o => o.Name.ToLower().Contains(searchingName.Trim().ToLower()));
        }
    }
    public class ClubParameters : QueryStringParameters
    {
        public string Name { get; set; }
    }
}