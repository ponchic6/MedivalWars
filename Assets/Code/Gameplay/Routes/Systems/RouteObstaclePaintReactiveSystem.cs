using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Routes.Systems
{
    public class RouteObstaclePaintReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;

        public RouteObstaclePaintReactiveSystem(IContext<GameEntity> context) : base(context)
        {
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.RouteIntersectingObstacle.AddedOrRemoved());

        protected override bool Filter(GameEntity entity) =>
            entity.hasRoutesColorController;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                if (entity.isRouteIntersectingObstacle)
                    entity.routesColorController.Value.SetIntersectingColor();
                else
                {
                    GameEntity startTower = _game.GetEntityWithId(entity.routeStartId.Value);
                    entity.routesColorController.Value.SetFractionColor(startTower.towerFraction.Value);
                }
            }
        }
    }
}