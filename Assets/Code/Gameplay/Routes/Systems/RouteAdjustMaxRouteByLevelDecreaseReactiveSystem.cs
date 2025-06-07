using System;
using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Routes.Systems
{
    public class RouteAdjustMaxRouteByLevelDecreaseReactiveSystem : ReactiveSystem<GameEntity>
    {
        public RouteAdjustMaxRouteByLevelDecreaseReactiveSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerLevel);

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities) 
                entity.ReplaceMaxRouteCount(entity.towerLevel.Value + 1);
        }
    }
}