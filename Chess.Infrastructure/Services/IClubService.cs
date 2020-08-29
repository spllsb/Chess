using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IClubService : IService
    {
        Task<ClubDto> GetClub (string name);
        Task<IEnumerable<ClubDto>> GetAllClubs ();
    }
}