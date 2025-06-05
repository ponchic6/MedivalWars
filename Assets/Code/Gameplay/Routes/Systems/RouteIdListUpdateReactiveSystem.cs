using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Routes.Systems
{
    public class RouteIdListUpdateReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;

        public RouteIdListUpdateReactiveSystem(IContext<GameEntity> context) : base(context)
        {
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Destructed.Added());

        protected override bool Filter(GameEntity entity) =>
            entity.isRoute && entity.hasRouteFinishId;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                GameEntity towerStart = _game.GetEntityWithId(entity.routeStartId.Value);
                towerStart.ReplaceUsedRouteCount(towerStart.usedRouteCount.Value - 1);
                towerStart.towerRouteIdList.Value.Remove(entity.id.Value);
                towerStart.ReplaceTowerRouteIdList(towerStart.towerRouteIdList.Value);
            }
        }
    }
}