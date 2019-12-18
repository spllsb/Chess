using System;
using System.Collections.Generic;

namespace Chess.Core.Domain
{
    public class Player
    {
        private ISet<PlayerTournamentParticipation> _playerTournamentParticipation = new HashSet<PlayerTournamentParticipation>(); 

        public Guid UserId { get; protected set; }
        public string Username { get; protected set; }

        protected Player()
        {
            PlayerTournamentParticipation = new HashSet<PlayerTournamentParticipation>();
        }
        public Player(User user)
        {
            UserId = user.Id;
            Username = user.Username;
        }
        public virtual IEnumerable<PlayerTournamentParticipation> PlayerTournamentParticipation 
        { 
            get { return _playerTournamentParticipation; }
            set { _playerTournamentParticipation = new HashSet<PlayerTournamentParticipation>(value);} 
        }

    }
}