using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Soldiers.Systems
{
    public class TowerFractionChangeViewReactiveSystem : ReactiveSystem<GameEntity>
    {
        public TowerFractionChangeViewReactiveSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerFraction);

        protected override bool Filter(GameEntity entity) =>
            entity.isTower;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity towerEntity in entities) 
                towerEntity.towerFractionColorController.Value.SetFractionColor(towerEntity.towerFraction.Value);
        }
    }
}