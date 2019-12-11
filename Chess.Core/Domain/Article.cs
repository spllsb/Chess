using System;
using System.Collections.Generic;

namespace Chess.Core.Domain
{
    public class Article
    {
        private ISet<Comment> _comments = new HashSet<Comment>();

        public Guid Id { get; protected set;}
        public string Title { get; protected set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FullNameAuthor { get; set; }

        public IEnumerable<Comment> Comments
        { 
            get { return _comments; } 
            set { _comments = new HashSet<Comment>(value); }
        }
        
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

        public void AddComment(string author, string content)
        {
            _comments.Add(Comment.Create(author,content));
            UpdatedAt = DateTime.UtcNow;
        }
    }
}