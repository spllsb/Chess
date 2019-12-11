using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository, IDatabaseRepository
    {
        private readonly MyDbContext _context;
        public ArticleRepository(MyDbContext context) 
        {
            _context = context;
        }
        public async Task<Article> GetAsync(Guid id)
            => await _context.Articles.SingleOrDefaultAsync(x => x.Id == id);

        // public async Task<Article> GetAsync(Guid id)
        // {
        //     var article = await _context.Articles.SingleOrDefaultAsync(x => x.Id == id);
        //     article.AddComment("asdasdasd","sadasdasdas");
        //     return article;
        // }
        public async Task<IEnumerable<Article>> GetAllAsync()
            => await _context.Articles.Take(10).ToListAsync();

        public async Task AddAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid id)
        {
            var article = await GetAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

 
    }
}