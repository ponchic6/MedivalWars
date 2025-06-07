using System.Collections.Generic;
using Code.Gameplay.Towers;
using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.EnemyAi.System
{
    public class EnemyRouteLineRendererSetterReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly CommonStaticData _commonStaticData;

        public EnemyRouteLineRendererSetterReactiveSystem(IContext<GameEntity> context, CommonStaticData commonStaticData) : base(context)
        {
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.LineRenderer.Added());

        protected override bool Filter(GameEntity entity)
        {
            if (!entity.isRoute)
                return false;

            GameEntity tower = _game.GetEntityWithId(entity.routeStartId.Value);

            return tower.towerFraction.Value != TowerFractionsEnum.Blue;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                GameEntity start = _game.GetEntityWithId(entity.routeStartId.Value);
                GameEntity end = _game.GetEntityWithId(entity.routeFinishId.Value);
                entity.lineRenderer.Value.SetPosition(0, start.transform.Value.position + Vector3.up * _commonStaticData.verticalRouteOffset);
                entity.lineRenderer.Value.SetPosition(1, end.transform.Value.position + Vector3.up * _commonStaticData.verticalRouteOffset);
                entity.AddRouteDistance(Vector3.Distance(start.transform.Value.transform.position, end.transform.Value.transform.position));
            }
        }
    }
}