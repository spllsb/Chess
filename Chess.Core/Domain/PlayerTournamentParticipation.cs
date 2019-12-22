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
    }
}