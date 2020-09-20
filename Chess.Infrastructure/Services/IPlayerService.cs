using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IPlayerService : IService
    {

        Task<PlayerDto> GetAsync(string username);
        Task<IEnumerable<PlayerDto>> GetAllAsync();
        Task <IEnumerable<PlayerDto>> PagedList(PlayerParameters parameters);
        Task <IEnumerable<PlayerDetailsDto>> GetPlayerMatches(Guid playerId);

        Task UpdateAsync(PlayerDto playerDto);
    }
}