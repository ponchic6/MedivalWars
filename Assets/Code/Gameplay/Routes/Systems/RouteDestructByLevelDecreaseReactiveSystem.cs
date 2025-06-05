using System;
using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Routes.Systems
{
    public class RouteDestructByLevelDecreaseReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private Random _random;

        public RouteDestructByLevelDecreaseReactiveSystem(IContext<GameEntity> context) : base(context)
        {
            _game = Contexts.sharedInstance.game;
            _random = new Random();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerLevel);

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                switch (entity.towerLevel.Value)
                {
                    case 0:
                    {
                        entity.ReplaceMaxRouteCount(1);
                        if (entity.towerRouteIdList.Value.Count > 1)
                            DestroyRandomRoute(entity);
                        break;
                    }
                    case 1:
                    {
                        entity.ReplaceMaxRouteCount(2);
                        if (entity.towerRouteIdList.Value.Count > 1)
                            DestroyRandomRoute(entity);
                        break;
                    }
                    case 2:
                    {
                        entity.ReplaceMaxRouteCount(3);
                        if (entity.towerRouteIdList.Value.Count > 1)
                            DestroyRandomRoute(entity);
                        break;
                    }
                }
            }
        }

        private void DestroyRandomRoute(GameEntity entity) =>
            _game.GetEntityWithId(entity.towerRouteIdList.Value[_random.Next(entity.towerRouteIdList.Value.Count)])
                .isDestructed = true;
    }
}