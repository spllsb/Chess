using System;

namespace Chess.Core.Domain
{
    public class Match
    {
        public Guid Id { get; set; }
        public Guid FirstPlayerId { get; protected set; }
        public Player FirstPlayer { get; protected set; }
        public Guid SecondPlayerId { get; protected set; }
        public Player SecondPlayer { get; protected set; }
        public DateTime BeginAt  { get; set; }
        // public MatchResultEnum Result { get; set; }
        public DateTime EndAt { get; set; }
        
        public string PgnPath {get; set; }

        // public MatchStatistic Statistic { get; set; }


        public void RegisterPlayer(Player newPlayer)
        {
 
        }

        protected Match(){}
        protected Match(Player player)
        {
            new Match(player);
            // Player = player;
        }
        public static Match Create(Player player)
            => new Match(player);
        
        private void CheckPlayerExistInMatch(Player newPlayer)
        {
            // CheckPlayer(newPlayer,Player);
        }
        private void CheckPlayer(Player newPlayer, Player player)
        {
            if (newPlayer.UserId == player?.UserId)
            {
                throw new Exception($"Player {newPlayer.UserId} is exists in match {this.Id}");
            }
        }
    }
}