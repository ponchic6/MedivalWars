using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Routes.Systems
{
    public class RoutePaintReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;

        public RoutePaintReactiveSystem(IContext<GameEntity> context) : base(context)
        {
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.RoutesColorController.Added());

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                GameEntity startTower = _game.GetEntityWithId(entity.routeStartId.Value);
                entity.routesColorController.Value.SetFractionColor(startTower.towerFraction.Value);
            }
        }
    }
}