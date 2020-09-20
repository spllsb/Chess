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
        public int Round { get; set; }

        public int FirstPlayerRating { get; set; }
        public int SecondPlayerRating { get; set; }
    }
}