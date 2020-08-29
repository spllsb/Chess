using System;

namespace Chess.Core.Domain
{
    public class Club
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public string ContactEmail { get; set; }
        
        protected Club()
        {
        }

        public Club(string name)
        {
            Name = name;
        }
    }
}