using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Towers.Systems
{
    public class TowerLevelViewReactiveSystem : ReactiveSystem<GameEntity>
    {
        public TowerLevelViewReactiveSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerLevel);

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                if (entity.towerLevel.Value == 0) 
                    entity.towerLevelViewController.Value.StartMorph(0);
                if (entity.towerLevel.Value == 1) 
                    entity.towerLevelViewController.Value.StartMorph(1);
                if (entity.towerLevel.Value == 2) 
                    entity.towerLevelViewController.Value.StartMorph(2);
            }
        }
    }
}