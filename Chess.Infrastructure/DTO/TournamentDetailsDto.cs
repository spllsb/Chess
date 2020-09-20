using System;
using System.Collections.Generic;
using Chess.Core.Domain;

namespace Chess.Infrastructure.DTO
{
    public class TournamentDetailsDto :TournamentDto
    {
        // public Guid TournamentId { get; set; }
        // public Guid PlayerId { get; set; }

        

        public IEnumerable<PlayerDto> Players { get; set; } 

        // public IEnumerable<MatchDto> Matches { get; set; }

    }
}