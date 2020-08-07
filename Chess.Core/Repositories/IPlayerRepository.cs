using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IPlayerRepository : IRepository
    {
        Task<Player> GetAsync(Guid id); 
        Task<Player> GetAsync(string username);
        Task<IEnumerable<Player>> GetAllAsync();
        IQueryable<Player> FindByCondition(Expression<Func<Player,bool>> expression);
        Task AddAsync(Player user);
        Task UpdateAsync(Player user);
        Task RemoveAsync(Guid id);
    }
}