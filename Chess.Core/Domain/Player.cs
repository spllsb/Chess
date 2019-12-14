using System;
using System.Collections.Generic;

namespace Chess.Core.Domain
{
    public class Player
    {
        // private ISet<Tournament> _tournaments = new HashSet<Tournament>(); 

        public Guid UserId { get; protected set; }
        public string Username { get; protected set; }

        protected Player()
        {
        }
        public Player(User user)
        {
            UserId = user.Id;
            Username = user.Username;
        }
        // public IEnumerable<Tournament> Tournaments 
        // { 
        //     get { return _tournaments; }
        //     set { _tournaments = new HashSet<Tournament>(value);} 
        // }

    }
}