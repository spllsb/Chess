using System;

namespace Chess.Infrastructure.DTO
{
    public class MatchDto
    {
        public Guid Id { get; protected set; }
        public DateTime BeginAt  { get; set; }
        
        public Guid FirstPlayerId{ get; protected set; }
        public PlayerDto FirstPlayer { get; set; }

        public Guid SecondPlayerId{ get; protected set; }
        public PlayerDto SecondPlayer { get; set; }
    }
}