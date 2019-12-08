using System;

namespace Chess.Core.Domain
{
    public class Article
    {
        public Guid Id { get; protected set;}
        public string Title { get; protected set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FullNameAuthor { get; set; }

        protected Article()
        {
        }
        public Article(string title,string content,string fullNameAuthor)
        {
            Title = title;
            Content = content;
            FullNameAuthor = fullNameAuthor;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}