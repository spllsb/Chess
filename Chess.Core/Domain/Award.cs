using System;

namespace Chess.Core.Domain
{
    public class Award
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
    }
}