using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Core.Domain
{
    public class Tournament
    {
        private ISet<PlayerTournamentParticipation> _players = new HashSet<PlayerTournamentParticipation>();

        public Guid Id { get; protected set;}
        public string Name { get; protected set; }
        public int MaxPlayers { get; protected set; }
        public DateTime UpdatedAt {get; private set;}
        public virtual IEnumerable<PlayerTournamentParticipation> Players 
        { 
            get { return _players;} 
            set { _players = new HashSet<PlayerTournamentParticipation>(value);} 
        }
        protected Tournament()
        {
        }

        public Tournament(string name, int maxPlayer)
        {
            Name = name;
            MaxPlayers = maxPlayer;
        }

        // public void AddPlayer(Player newPlayer)
        // {
        //     var user = _players.SingleOrDefault(x => x.UserId == newPlayer.UserId);
        //     if (user != null)
        //     {
        //         throw new Exception($"User with id: '{newPlayer.UserId}' and username '{newPlayer.Username}' already exists for tournament. You can't add him again");
        //     }
        //     _players.Add(newPlayer);
        //     UpdatedAt = DateTime.UtcNow;
        // }

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