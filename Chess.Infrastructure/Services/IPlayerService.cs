using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IPlayerService : IService
    {
        Task<PlayerDto> GetAsync(string email);
        Task<IEnumerable<PlayerDto>> GetAllAsync();
    }
}