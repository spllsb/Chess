using System;

namespace Chess.Core.Domain
{
    public class Match
    {
        public Guid Id { get; set; }
        // public Guid FirstPlayerId { get; protected set; }
        // public Guid SecondPlayerId { get; protected set; }
        public Guid PlayerId { get; protected set; }
        public Player Player { get; protected set; }
        public DateTime BeginAt  { get; set; }
        // public MatchResultEnum Result { get; set; }
        public DateTime EndAt { get; set; }

        // public MatchStatistic Statistic { get; set; }


        public void RegisterPlayer(Player newPlayer)
        {
            CheckPlayerExistInMatch(newPlayer);
            if(Player != null)
            {
                Player = newPlayer;
            }
        }

        protected Match(){}
        protected Match(Player player)
        {
            Id = Guid.NewGuid();
            Player = player;
        }
        public static Match Create(Player player)
            => new Match(player);
        
        private void CheckPlayerExistInMatch(Player newPlayer)
        {
            CheckPlayer(newPlayer,Player);
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