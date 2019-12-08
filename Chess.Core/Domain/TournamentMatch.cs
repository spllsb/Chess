namespace Chess.Core.Domain
{
    public class TournamentMatch : Match
    {
        public Tournament TournamentId { get; set; }
        public TournamentMatch() : base()
        {
        }

    }
}