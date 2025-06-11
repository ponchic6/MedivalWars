using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Routes.Systems
{
    public class RouteMaxCountViewReactiveSystem : ReactiveSystem<GameEntity>
    {
        public RouteMaxCountViewReactiveSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.MaxRouteCount);

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
                entity.towerUiView.Value.SetMaxRoutes(entity.maxRouteCount.Value);
        }
    }
}