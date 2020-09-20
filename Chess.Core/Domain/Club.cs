using System;
using System.Collections.Generic;

namespace Chess.Core.Domain
{
    public class Club
    {
        private ISet<Player> _players = new HashSet<Player>();
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public string ContactEmail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string PictureName { get; set; }
        public string Information { get; set; }
    

        protected Club()
        {
        }

        public Club(string name)
        {
            Name = name;
        }

        public Club(string name, string contactEmail, string pictureName, string information)
        {
            Name = name;
            ContactEmail = contactEmail;
            PictureName = pictureName;
            Information = information;
        }

        public void Upadate(string name, string contactEmail, string pictureName, string information)
        {
            Name = name;
        }

        public virtual IEnumerable<Player> Players 
        { 
            get { return _players;} 
            set { _players = new HashSet<Player>(value);} 
        }
    }
}