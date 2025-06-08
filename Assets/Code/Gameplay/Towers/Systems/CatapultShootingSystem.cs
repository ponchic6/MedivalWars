using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Towers.Services;
using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Towers.Systems
{
    public class CatapultShootingSystem : IExecuteSystem
    {
        private readonly IProjectileFactory _projectileFactory;
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;
        private List<GameEntity> _buffer = new(16);
        private List<GameEntity> _targetsBuffer = new(128);
        private System.Random _random = new();

        public CatapultShootingSystem(IProjectileFactory projectileFactory, CommonStaticData commonStaticData)
        {
            _projectileFactory = projectileFactory;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.ReadyToShooting);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                GameEntity possibleTarget = GetTarget(entity);
                
                if (possibleTarget == null)
                    continue;
                
                entity.isReadyToShooting = false;
                
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
                
                _projectileFactory.CreateProjectile(entity, possibleTarget);
            }
        }

        private GameEntity GetTarget(GameEntity entity)
        {
            List<GameEntity> possibleTargets = _game
                .GetGroup(GameMatcher.TowerFraction)
                .GetEntities(_targetsBuffer)
                .Where(x => x.towerFraction.Value != entity.towerFraction.Value)
                .Where(x =>
                {
                    float catapultRange = entity.towerLevel.Value switch
                    {
                        0 => _commonStaticData.catapultRangeZeroLevel,
                        1 => _commonStaticData.catapultRangeFirstLevel,
                        2 => _commonStaticData.catapultRangeSecondLevel
                    };
                    
                    return Vector3.Distance(entity.transform.Value.position, x.transform.Value.position) < catapultRange;
                })
                .ToList();

            GameEntity possibleTarget = possibleTargets.Any() 
                ? possibleTargets[_random.Next(possibleTargets.Count)]
                : null;
            
            return possibleTarget;
        }
    }
}