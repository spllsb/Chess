using System;
using System.Threading.Tasks;

namespace Chess.Infrastructure.Services
{
    public interface IArticleCommentService : IService
    {
        Task AddAsync(Guid articleId, string author, string content);
    }
}