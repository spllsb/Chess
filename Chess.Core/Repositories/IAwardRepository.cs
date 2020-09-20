using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IAwardRepository : IRepository
    {
        Task<Award> GetAsync(string name); 
        Task<IEnumerable<Award>> GetAllAsync();
        IQueryable<Award> FindByCondition(Expression<Func<Award,bool>> expression);
    }
}