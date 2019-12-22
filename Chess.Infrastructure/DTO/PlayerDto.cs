using System;

namespace Chess.Infrastructure.DTO
{
    public class PlayerDto
    {
        public Guid PlayerId { get; set; }
        public string Username { get; set; }
    }
}