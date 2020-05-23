using System;
using System.Collections.Generic;

namespace Chess.Core.Domain
{
    public class Room
    {
        public Guid Id { get; }
        public DateTime CreateAt { get; set; }
        public IEnumerable<Player> Players { get; set; }

        public Room()
        {
            this.Id = new Guid();
            this.CreateAt = DateTime.UtcNow;
        }
    }
}