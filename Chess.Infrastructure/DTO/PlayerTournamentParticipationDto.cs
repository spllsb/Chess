using System;

namespace Chess.Infrastructure.DTO
{
    public class PlayerTournamentParticipationDto
    {
        public TournamentDto Tournament { get; set; }
        public Guid TournamentId { get; set; }
        public PlayerDto Player {get;set;}
        public Guid PlayerId { get; set; }
        public string Result { get; set; }
    }
}