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
        protected PlayerTournamentParticipation(Guid playerId, Guid tournamentId)
        {
            PlayerId = playerId;
            TournamentId = tournamentId;
        }
        public static PlayerTournamentParticipation Create(Guid playerId, Guid tournamentId)
            => new PlayerTournamentParticipation(playerId,tournamentId);
    }
}