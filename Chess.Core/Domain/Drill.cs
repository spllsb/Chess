using System;
using System.Collections.Generic;

namespace Chess.Core.Domain
{
    public class Drill
    {
        private ISet<PlayerDrillParticipation> _players = new HashSet<PlayerDrillParticipation>();

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Category { get; protected set; }
        public string StartPosition { get; protected set;}
        public string FileName { get; protected set;}
        protected Drill()
        {
        }
        public Drill(string name, string description, string category)
        {
            Name = name;
            Description = description;
            Category = category;
        }
        public virtual IEnumerable<PlayerDrillParticipation> Players 
        { 
            get { return _players;} 
            set { _players = new HashSet<PlayerDrillParticipation>(value);} 
        }
    }
}