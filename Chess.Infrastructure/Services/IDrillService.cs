using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain.Enum;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IDrillService : IService
    {
        Task<IEnumerable<DrillDto>> GetAllCategoryElementAsync(string category);
        Task<DrillDto> GetAsync(Guid id);
        Task <IEnumerable<DrillDto>> GetDrillAsync();
        Task <DrillDto> GetRandomDrillAsync();

        Task AddPlayed(Guid drillId, Guid playerId, DrillResultTypeEnum result);
    }
}