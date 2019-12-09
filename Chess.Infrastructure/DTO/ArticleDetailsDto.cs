using System.Collections;
using System.Collections.Generic;

namespace Chess.Infrastructure.DTO
{
    public class ArticleDetailsDto : ArticleDto
    {
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}