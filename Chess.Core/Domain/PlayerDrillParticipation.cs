using System;
using Chess.Core.Domain.Enum;

namespace Chess.Core.Domain
{
    public class PlayerDrillParticipation
    {
        public Guid Id { get; set; }
        public Guid DrillId { get; set; }
        public virtual Drill Drill { get; set; }

        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public string Result { get; set; }  

        protected PlayerDrillParticipation()
        {}
        protected PlayerDrillParticipation(Guid playerId, Guid drillId)
        {
            PlayerId = playerId;
            DrillId = drillId;
        }
        protected PlayerDrillParticipation(Guid playerId, Guid drillId, DrillResultTypeEnum result)
        {
            PlayerId = playerId;
            DrillId = drillId;
            Result = result.ToString();
        }
        public static PlayerDrillParticipation Create(Guid playerId, Guid drillId)
            => new PlayerDrillParticipation(playerId, drillId);

        public static PlayerDrillParticipation Create(Guid playerId, Guid drillId, DrillResultTypeEnum result)
            => new PlayerDrillParticipation(playerId, drillId, result);
    }
}