using System;
using System.ComponentModel.DataAnnotations;

namespace Chess.Infrastructure.DTO
{
    public class MatchDto
    {
        public Guid Id { get; protected set; }

        [DataType(DataType.Date)]
        public DateTime BeginAt  { get; set; }
        
        public Guid FirstPlayerId{ get; protected set; }
        public PlayerDto FirstPlayer { get; set; }

        public Guid SecondPlayerId{ get; protected set; }
        public PlayerDto SecondPlayer { get; set; }

        public string Fen { get; set; }
        public string PgnFileName { get; set; }
        public string Result { get; set; }
    }
}