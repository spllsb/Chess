using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Core.Domain
{
    public class Tournament
    {
        private ISet<PlayerTournamentParticipation> _players = new HashSet<PlayerTournamentParticipation>();
        // private ISet<Match> _matches = new HashSet<Match>();

        public Guid Id { get; protected set;}
        public string Name { get; protected set; }
        public int MaxPlayers { get; protected set; }
        public string ClubName { get; protected set; }
        public string GameTyp { get; protected set; }
        public DateTime StartEvent { get; protected set; }
        public int RoundAmount { get; protected set; }
        public int MinRank { get; protected set; }
        public int MaxRank { get; protected set; }
        public int MinPlayers { get; protected set; }
        public DateTime UpdatedAt {get; private set;}
        public virtual IEnumerable<PlayerTournamentParticipation> Players 
        { 
            get { return _players;} 
            set { _players = new HashSet<PlayerTournamentParticipation>(value);} 
        }

        // public virtual IEnumerable<Match> Matches
        // {
        //     get { return _matches;} 
        //     set { _matches = new HashSet<Match>(value);} 
        // }


        protected Tournament()
        {
        }

        public Tournament(string name, int maxPlayer)
        {
            Name = name;
            MaxPlayers = maxPlayer;
        }

        // public void DeletePlayer(User deleteUser)
        // {
        //     var user = Users.SingleOrDefault(x => x.Id == deleteUser.Id);
        //     if (user == null)
        //     {
        //         throw new Exception($"User with id: '{deleteUser.Id}' and email '{deleteUser.Email}' was not found");
        //     }
        //     _players.Remove(deleteUser);
        //     UpdatedAt = DateTime.UtcNow;
        // }

    }
}