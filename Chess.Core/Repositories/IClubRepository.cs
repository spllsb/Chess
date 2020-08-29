using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IClubRepository : IRepository
    {
        Task<Club> GetAsync(string name);
        Task<IEnumerable<Club>> GetAllAsync();
    }
}