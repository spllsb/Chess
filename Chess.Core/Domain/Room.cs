using System;
using System.Collections.Generic;

namespace Chess.Core.Domain
{
    public class Room
    {
        public Guid Id { get; }
        public DateTime CreateAt { get; set; }
        public IEnumerable<Player> Players { get; set; }

        private Room()
        {
            this.Id = Guid.NewGuid();
            this.CreateAt = DateTime.UtcNow;
        }

        public static Room CreateRoom()
            => new Room();


    }
}