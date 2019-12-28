using System;

namespace Chess.Core.Domain
{
    //participation - uczestnictwo
    public class PlayerTournamentParticipation
    {
        public Guid TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public string Result { get; set; }  

        protected PlayerTournamentParticipation()
        {}
        protected PlayerTournamentParticipation(Player player, Tournament tournament)
        {
            Player = player;
            Tournament = tournament;
        }
        public static PlayerTournamentParticipation Create(Player player, Tournament tournament)
            => new PlayerTournamentParticipation(player,tournament);
    }
}