using System;

namespace Chess.Core.Domain
{
    public abstract class Match
    {
        public User PlayerId1 { get; protected set; }
        public User PlayerId2 { get; protected set; }
        public DateTime BeginAt  { get; set; }
        public MatchResultEnum Result { get; set; }
        public DateTime EndAt { get; set; }
    
    }
}