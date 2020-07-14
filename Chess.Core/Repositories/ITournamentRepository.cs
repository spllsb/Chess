using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface ITournamentRepository : IRepository
    {
        Task<Tournament> GetAsync(Guid id); 
        Task<Tournament> GetAsync(string name); 
        Task<Tournament> GetTournamentAsync(string name);
        Task<IEnumerable<Tournament>> GetAllAsync();

        IQueryable<Tournament> FindByCondition(Expression<Func<Tournament,bool>> expression);
        Task AddAsync(Tournament tournament);
        Task UpdateAsync(Tournament tournament);
        Task RemoveAsync(Guid id);
    }
}