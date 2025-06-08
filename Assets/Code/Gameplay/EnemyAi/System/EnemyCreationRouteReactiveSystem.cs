using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Routes.Services;
using Code.Gameplay.Towers;
using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;
using Random = System.Random;

namespace Code.Gameplay.EnemyAi.System
{
    public class EnemyCreationRouteReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly IRouteFactory _routeFactory;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;
        private readonly Random _random;
        private readonly CommonStaticData _commonStaticData;

        public EnemyCreationRouteReactiveSystem(IContext<GameEntity> context, IRouteFactory routeFactory, CommonStaticData commonStaticData) : base(context)
        {
            _routeFactory = routeFactory;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
            _random = new Random();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.EnemyReadyToAction.Added());

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            _game.enemyActionCooldownEntity.ReplaceEnemyActionCooldown(_commonStaticData.enemyActionCooldown);
            _game.enemyActionCooldownEntity.isEnemyReadyToAction = false;

            List<GameEntity> filteredTowers = _game
                .GetGroup(GameMatcher.AllOf(GameMatcher.TowerFraction, GameMatcher.Tower))
                .GetEntities()
                .Where(x => x.towerFraction.Value == TowerFractionsEnum.Red)
                .Where(x => x.usedRouteCount.Value < x.maxRouteCount.Value)
                .ToList();

            GameEntity randomEnemyTower = filteredTowers.Any() 
                ? filteredTowers[_random.Next(filteredTowers.Count)]
                : null;

            if (randomEnemyTower == null)
                return;

            List<GameEntity> destinationTowers = _game
                .GetGroup(GameMatcher.AllOf(GameMatcher.TowerFraction, GameMatcher.Tower))
                .GetEntities()
                .Where(x => x != randomEnemyTower)
                .Where(x =>
                {
                    foreach (int routeId in randomEnemyTower.towerRouteIdList.Value)
                    {
                        if (_game.GetEntityWithId(routeId).routeFinishId.Value == x.id.Value)
                            return false;
                    }
                    return true;
                })
                .Where(x => !HasObstacleBetween(randomEnemyTower, x))
                .ToList();

            GameEntity destination = destinationTowers.Any() 
                ? destinationTowers[_random.Next(destinationTowers.Count)]
                : null;
            
            if (destination == null)
                return;
            
            _routeFactory.CreateRoute(randomEnemyTower, destination);
        }

        private bool HasObstacleBetween(GameEntity start, GameEntity end)
        {
            Vector3 startPos = start.transform.Value.position;
            Vector3 endPos = end.transform.Value.position;
            
            Vector3 direction = (endPos - startPos).normalized;
            float distance = Vector3.Distance(startPos, endPos);
            
            int obstacleLayerMask = 1 << LayerMask.NameToLayer("Obstacles");
            
            if (Physics.Raycast(startPos, direction.normalized, out _, distance, obstacleLayerMask))
                return true;
            
            return false;
        }
    }
}