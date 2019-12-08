using System;

namespace Chess.Core.Domain
{
    //participation - uczestnictwo
    public class PlayerTournamentParticipation
    {
        public Tournament TournamentId { get; protected set; }
        public User PlayerId { get; protected set; }
        public string Result { get; set; }
    }
}