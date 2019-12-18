using System.Collections.Generic;
using Chess.Core.Domain;

namespace Chess.Infrastructure.DTO
{
    public class TournamentDetailsDto :TournamentDto
    {
        public IEnumerable<PlayerTournamentParticipationDto> Players { get; set; } 
    }
}