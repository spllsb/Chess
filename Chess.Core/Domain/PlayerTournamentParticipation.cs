using System;

namespace Chess.Core.Domain
{
    //participation - uczestnictwo
    public class PlayerTournamentParticipation
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        // public Tournament Tournament { get; protected set; }
        
        public Guid PlayerId { get; set; }
        // public Player Player { get; protected set; }

        public string Result { get; set; }
    }
}