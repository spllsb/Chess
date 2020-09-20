using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Services;

namespace Chess.Infrastructure.Services
{
    public interface IClubService : IService
    {
        Task CreateAsync(string name, string contactEmail, string pictureName, string information);
        Task<ClubDetailsDto> GetAsync (string name);
        Task<IEnumerable<ClubDto>> GetAllClubs();
        Task <IEnumerable<ClubDto>> PagedList(ClubParameters clubParameters);
        Task<ClubDetailsDto> GetAsync(Guid clubId);
        Task UpdateAsync(ClubDetailsDto clubId);
        
    }
}