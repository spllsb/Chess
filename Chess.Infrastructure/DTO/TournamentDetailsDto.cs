using System.Collections.Generic;

namespace Chess.Infrastructure.DTO
{
    public class TournamentDetailsDto :TournamentDto
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}