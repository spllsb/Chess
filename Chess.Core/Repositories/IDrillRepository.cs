using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IDrillRepository : IRepository
    {
        Task<IEnumerable<Drill>> GetAllByCategoryAsync(string category);
        Task <Drill> GetAsync(Guid id);
        IQueryable<Drill> FindByCondition(Expression<Func<Drill,bool>> expression);

        Task Update(Drill drill);
    }
}