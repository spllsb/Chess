using System;

namespace Chess.Core.Domain
{
    public class PlayerAward
    {
        public Guid Id { get; set; }
        public Guid AwardId { get; set; }
        public virtual Award Award { get; set; }

        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public DateTime AddAt { get; set; }  = DateTime.UtcNow;

        protected PlayerAward()
        {}
        protected PlayerAward(Guid playerId, Guid awardId)
        {
            PlayerId = playerId;
            AwardId = awardId;
        }
        public static PlayerAward Create(Guid playerId, Guid awardId)
            => new PlayerAward(playerId,awardId);
    }
}