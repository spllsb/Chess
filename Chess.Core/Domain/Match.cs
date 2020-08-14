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
        public DateTime BeginAt  { get; set; } = DateTime.Now;
        // public MatchResultEnum Result { get; set; }
        public DateTime EndAt { get; set; } = DateTime.Now;
        
        public string PgnPath {get; set; } = "aa";
        public Guid TournamentId { get; set; } 
        public string Fen { get; set; }

        // public MatchStatistic Statistic { get; set; }


        public void RegisterPlayer(Player newPlayer)
        {
 
        }

        protected Match(){}
        protected Match(Player player)
        {
            
            // Player = player;
        }

        protected Match(Guid playerOne, Guid playerTwo)
        {
            FirstPlayerId = playerOne;
            SecondPlayerId = playerTwo;
            // Player = player;
        }
        public static Match Create(Player player)
            => new Match(player);

        public static Match Create(Guid playerOne, Guid playerTwo)
            => new Match(playerOne, playerTwo);
        
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