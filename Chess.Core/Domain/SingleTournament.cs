using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Core.Domain
{
    public class SingleTournament
    {
        private ISet<Player> _players = new HashSet<Player>();
        private ISet<Match> _matches = new HashSet<Match>();

        public TournamentStatistic Statistic { get; protected set; } 
        public Tournament Tournament {get; protected set;}
        public IEnumerable<Player> Players 
        { 
            get 
            { 
                return _players;
            }
            set 
            {
                _players = new HashSet<Player>(value);
            } 
        }
        public IEnumerable<Match> Matches
        { 
            get 
            { 
                return _matches;
            }
            set 
            {
                _matches = new HashSet<Match>(value);
            } 
        }


        public void AddPlayer(Player newPlayer)
        {
            var player = GetPlayerTournamentParticipation(newPlayer);
            if (player != null)
            {
                throw new Exception($"User with id: '{newPlayer.UserId}' and username '{newPlayer.UserName}' already exists for tournament. You can't add him again");
            }
            _players.Add(newPlayer);
        }

        private Player GetPlayerTournamentParticipation(Player player)
            => _players.SingleOrDefault(x => x.UserId == player.UserId);
    }
}