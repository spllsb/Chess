using System.Collections.Generic;

namespace Chess.Infrastructure.DTO
{
    public class PlayerDetailsDto : PlayerDto
    {
        // public IEnumerable<MatchDto> Matches { get; set; }
        public int ResolveDrillsCount { get; set; }
        public int CorrectResolveDrillsCount { get; set; }
    }
}