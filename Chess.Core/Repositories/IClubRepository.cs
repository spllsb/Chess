using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IClubRepository : IRepository
    {
        Task<Club> GetAsync(string name);
        Task<Club> GetAsync(Guid id);
        Task<IEnumerable<Club>> GetAllAsync();
        IQueryable<Club> FindByCondition(Expression<Func<Club,bool>> expression);
        Task UpdateAsync(Club club);
        Task AddAsync(Club club);
    }
}