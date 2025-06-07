using Entitas;
using UnityEngine;

namespace Code.Gameplay.EnemyAi.System
{
    public class EnemyCooldownActionSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public EnemyCooldownActionSystem()
        {
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.EnemyActionCooldown);
        }
        
        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.isEnemyReadyToAction)
                    continue;
                
                if (entity.enemyActionCooldown.Value <= 0 && !entity.isEnemyReadyToAction) 
                    entity.isEnemyReadyToAction= true;
                
                entity.enemyActionCooldown.Value -= Time.deltaTime;
            }
        }
    }
}