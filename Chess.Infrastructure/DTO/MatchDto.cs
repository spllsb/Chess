using System;

namespace Chess.Infrastructure.DTO
{
    public class MatchDto
    {
        public Guid Id { get; protected set; }
        public DateTime BeginAt  { get; set; }
        
    }
}