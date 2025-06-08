using Entitas;
using UnityEngine;

namespace Code.Gameplay.Towers.Systems
{
    public class CatapultShootingCooldownSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public CatapultShootingCooldownSystem()
        {
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.CatapultShootingCooldown);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.isReadyToShooting)
                    continue;
                
                if (entity.catapultShootingCooldown.Value <= 0 && !entity.isReadyToShooting) 
                    entity.isReadyToShooting= true;
                
                entity.catapultShootingCooldown.Value -= Time.deltaTime;
            }
        }
    }
}