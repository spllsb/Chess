using System;
using System.Collections.Generic;

namespace Chess.Core.Domain
{
    public class Award
    {
        private ISet<PlayerAward> _players = new HashSet<PlayerAward>();

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string FileName { get; protected set; }
        public string Category { get; protected set; }
        public string Comment { get; protected set; }


        public virtual IEnumerable<PlayerAward> Players 
        { 
            get { return _players;} 
            set { _players = new HashSet<PlayerAward>(value);} 
        }
    }
}