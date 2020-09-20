using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Core.Domain
{
    public class Player
    {
        private ISet<PlayerTournamentParticipation> _tournaments = new HashSet<PlayerTournamentParticipation>(); 
        private ISet<PlayerDrillParticipation> _drills = new HashSet<PlayerDrillParticipation>(); 
        public Guid UserId { get; protected set; }
        public Guid ClubId {get; protected set;}
        public string Email { get; protected set; }
        public int RatingElo { get; set; }
        public string AvatarImageName { get; set; }
        public string UserName { get; set; }




        public virtual IEnumerable<PlayerTournamentParticipation> Tournaments 
        { 
            get { return _tournaments; }
            set { _tournaments = new HashSet<PlayerTournamentParticipation>(value);} 
        }
 
        public virtual IEnumerable<PlayerDrillParticipation> Drills 
        { 
            get { return _drills; }
            set { _drills = new HashSet<PlayerDrillParticipation>(value);} 
        }

        protected Player()
        {
        }
        public Player(ApplicationUser user)
        {
            UserId = new Guid(user.Id);
            UserName = user.UserName;
            Email = user.Email;
        }

        public void Update(string email, int ratingElo, string avatarImageName, string userName){
            Email = email;
            RatingElo = ratingElo;
            AvatarImageName = avatarImageName;
            UserName = userName;
        }
    }
}