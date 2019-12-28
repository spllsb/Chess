using System;

namespace Chess.Core.Domain
{
    public class Match
    {
        public Guid Id { get; set; }
        public Player Player1 { get; protected set; }
        public Player Player2 { get; protected set; }
        public DateTime BeginAt  { get; set; }
        public MatchResultEnum Result { get; set; }
        public DateTime EndAt { get; set; }
        public bool IsFull { get; protected set; }

        public MatchStatistic Statistic { get; set; }


        public void RegisterPlayer(Player newPlayer)
        {
            CheckPlayerExistInMatch(newPlayer);
            if(Player1 != null)
            {
                Player1 = newPlayer;
            }
            else if(Player2 != null)
            {
                Player2 = newPlayer;
            }
        }

        protected Match(){}
        protected Match(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }
        public static Match Create(Player player1, Player player2)
            => new Match(player1,player2);
        
        private void CheckPlayerExistInMatch(Player newPlayer)
        {
            CheckPlayer(newPlayer,Player1);
            CheckPlayer(newPlayer,Player2);
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