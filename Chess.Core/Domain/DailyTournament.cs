using System.Collections.Generic;

namespace Chess.Core.Domain
{
    public class DailyTournament : Match
    {

        private ISet<Match> _matches = new HashSet<Match>();
        public Tournament Tournament { get; set; }
        public virtual IEnumerable<Match> Matches 
        { 
            get { return _matches;} 
            set { _matches = new HashSet<Match>(value);} 
        }

        public DailyTournament() : base()
        {
        }

        public void AddMatch(Player player){
            _matches.Add(Match.Create(player));
        }

    }
}