using System;

namespace Chess.Infrastructure.DTO
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get;  set; }
        public string Content { get; set; }

        public string FullNameAuthor { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}