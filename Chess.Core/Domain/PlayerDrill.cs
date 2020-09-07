using System;

namespace Chess.Core.Domain
{
    public class PlayerDrill
    {
        
        public Guid DrillId { get; set; }
        public virtual Drill Drill { get; set; }  

        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public string Result { get; set; }  
    }
}