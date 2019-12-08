using System;

namespace Chess.Infrastructure.DTO
{
    public class ArticleDto
    {
        public string Title { get; protected set; }
        public string Content { get; set; }

        public string FullNameAuthor { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}