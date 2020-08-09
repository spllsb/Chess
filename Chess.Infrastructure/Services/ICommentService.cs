using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface ICommentService
    {
         Task<IEnumerable<CommentDto>> GetCommentAsync(Guid articleKey);
         Task AddAsync(Guid articleKey, Comment comment);
    }
}