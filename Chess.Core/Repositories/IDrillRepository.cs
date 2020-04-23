using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IDrillRepository : IRepository
    {
        Task<IEnumerable<Drill>> GetAllByCategoryAsync(string category);
        Task <Drill> GetAsync(int id);
    }
}