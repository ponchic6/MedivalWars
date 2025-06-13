using System.Collections.Generic;
using Code.Gameplay.Towers.Services;
using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;
using Random = System.Random;

namespace Code.Gameplay.Towers.Systems
{
    public class CatapultProjectileCreationSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;
        private readonly Random _random;
        private readonly CommonStaticData _commonStaticData;
        private readonly IProjectileFactory _projectileFactory;
        private List<GameEntity> _targetsBuffer = new(128);

        public CatapultProjectileCreationSystem(CommonStaticData commonStaticData, IProjectileFactory projectileFactory)
        {
            _random = new();
            _commonStaticData = commonStaticData;
            _projectileFactory = projectileFactory;
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.CatapultShootingCooldown);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.catapultShootingCooldown.Value <= 0)
                {
                    GameEntity possibleTarget = GetTarget(entity);
                
                    if (possibleTarget == null)
                        continue;
                    
                    RefreshCooldown(entity);
                    _projectileFactory.CreateProjectile(entity, possibleTarget);
                }
                
                entity.catapultShootingCooldown.Value -= Time.deltaTime;
            }
        }

        private void RefreshCooldown(GameEntity entity)
        {
            switch (entity.towerLevel.Value)
            {
                case 0:
                    entity.ReplaceCatapultShootingCooldown(_commonStaticData.catapultCooldownZeroLevel);
                    break; 
                case 1:
                    entity.ReplaceCatapultShootingCooldown(_commonStaticData.catapultCooldownFirstLevel);
                    break;
                case 2:
                    entity.ReplaceCatapultShootingCooldown(_commonStaticData.catapultCooldownSecondLevel);
                    break;
            }
        }

        private GameEntity GetTarget(GameEntity entity)
        {
            var entities = _game
                .GetGroup(GameMatcher.AllOf(GameMatcher.TowerFraction, GameMatcher.Transform))
                .GetEntities(_targetsBuffer);
    
            float catapultRange = entity.towerLevel.Value switch
            {
                0 => _commonStaticData.catapultRangeZeroLevel,
                1 => _commonStaticData.catapultRangeFirstLevel,
                2 => _commonStaticData.catapultRangeSecondLevel
            };
    
            TowerFractionsEnum catapultFraction = entity.towerFraction.Value;
            Vector3 catapultPos = entity.transform.Value.position;
    
            GameEntity selectedTarget = null;
            int validTargetsCount = 0;
    
            foreach (GameEntity target in entities)
            {
                if (target.towerFraction.Value == catapultFraction || Vector3.Distance(catapultPos, target.transform.Value.position) >= catapultRange)
                    continue;
                
                if (target.hasSoldierHealth && target.soldierHealth.Value <= 0)
                    continue;
                
                validTargetsCount++;
                if (_random.Next(validTargetsCount) == 0) 
                    selectedTarget = target;
            }
    
            return selectedTarget;
        }
    }
}