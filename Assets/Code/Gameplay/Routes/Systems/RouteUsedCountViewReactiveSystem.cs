using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Routes.Systems
{
    public class RouteUsedCountViewReactiveSystem : ReactiveSystem<GameEntity>
    {
        public RouteUsedCountViewReactiveSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.UsedRouteCount);

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
                entity.towerUiView.Value.SetUsedRoutes(entity.usedRouteCount.Value);
        }
    }
}