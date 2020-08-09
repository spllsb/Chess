using System;

namespace Chess.Infrastructure.DTO
{
    public class TournamentDto
    {
        public Guid Id { get; set;}
        public string Name { get; set; }
        public DateTime StartEvent { get; set; }
        public int MaxPlayers { get; set; }
        public int MinRank { get; set; }
        public int MaxRank { get; set; }

    }
}