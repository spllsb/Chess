using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Core.Domain
{
    public class Player
    {
        private ISet<PlayerTournamentParticipation> _tournaments = new HashSet<PlayerTournamentParticipation>(); 
        public Guid UserId { get; protected set; }
        public string Username { get; protected set; }
        public Guid ClubId {get; protected set;}
        public string Email { get; protected set; }
        public virtual IEnumerable<PlayerTournamentParticipation> Tournaments 
        { 
            get { return _tournaments; }
            set { _tournaments = new HashSet<PlayerTournamentParticipation>(value);} 
        }
 
    
        protected Player()
        {
        }
        public Player(User user)
        {
            UserId = user.Id;
            Username = user.Username;
        }
    }
}