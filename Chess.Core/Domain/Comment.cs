using System;

namespace Chess.Core.Domain
{
    public class Comment
    {
        public Guid Id { get; protected set;}
        //pozniej to bedzie uzytkownik
        public string Author { get; set; }
        public string Content { get; set; }

    }
}