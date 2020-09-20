using System.Collections.Generic;

namespace Chess.Infrastructure.DTO
{
    public class ClubDetailsDto : ClubDto
    {
        public IEnumerable<PlayerDto> Players { get; set; }
        public string Information { get; set; }

    }
}