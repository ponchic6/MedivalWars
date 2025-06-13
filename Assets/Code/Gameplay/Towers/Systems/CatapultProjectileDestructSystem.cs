using Entitas;
using UnityEngine;

namespace Code.Gameplay.Towers.Systems
{
    public class CatapultProjectileDestructSystem : IExecuteSystem
    {
        private const float distanceForDestruct = 0.2f;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public CatapultProjectileDestructSystem()
        {
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.Projectile, GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (GameEntity projectile in _entities)
            {
                GameEntity target = _game.GetEntityWithId(projectile.projectileTargetId.Value);

                if (target == null)
                {
                    projectile.isDestructed = true;
                    continue;
                }

                if (Vector3.Distance(target.transform.Value.position, projectile.transform.Value.position) > distanceForDestruct)
                    continue;
                
                if (target.hasSoldierAttackTowerId)
                {
                    projectile.isDestructed = true;
                    target.ReplaceSoldierHealth(target.soldierHealth.Value - 2);
                    continue;
                }

                if (!target.isTower)
                    continue;

                if (target.towerScore.Value > 0)
                {
                    target.towerScore.Value--;
                    target.ReplaceTowerScore(target.towerScore.Value);
                }

                projectile.isDestructed = true;
            }
        }
    }
}