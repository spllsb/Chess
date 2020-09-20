using System;

namespace Chess.Infrastructure.DTO
{
    public class DrillDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string StartPosition { get; set; }
        public string FileName { get; set; }

        public int CorrectlyAttempts { get; set; }
        public int Attempts { get; set; }

    }
}