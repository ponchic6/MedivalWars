using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Soldiers.Systems
{
    public class DestructZeroHpSoldierRs : ReactiveSystem<GameEntity>
    {
        public DestructZeroHpSoldierRs(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.SoldierHealth);

        protected override bool Filter(GameEntity entity) =>
            entity.hasSoldierHealth && entity.soldierHealth.Value <= 0;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities) 
                entity.isDestructed = true;
        }
    }
}