using System;

namespace Chess.Core.Domain
{
    public class Comment
    {
        public Guid Id { get; protected set;}
        public Guid ArticleId { get; set; }
        //pozniej to bedzie uzytkownik
        public string Author { get; set; }
        public string Content { get; set; }


        public virtual Article Article { get; set; }

        protected Comment()
        {}
        protected Comment(string author, string content)
        {
            Author = author;
            Content = content;
        }

        public static Comment Create(string author, string content)
            => new Comment(author,content);
    }
}