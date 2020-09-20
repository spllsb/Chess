using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IAwardService : IService
    {
        Task <AwardDto> GetAsync(string name);
        Task <IEnumerable<AwardDto>> GetAllAsync(); 
        Task<IEnumerable<AwardDto>> GetAllByCategoryAsync(string category);
    }
}