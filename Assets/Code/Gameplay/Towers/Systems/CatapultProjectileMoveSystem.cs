using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Towers.Systems
{
    public class CatapultProjectileMoveSystem : IExecuteSystem
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public CatapultProjectileMoveSystem(CommonStaticData commonStaticData)
        {
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.Projectile, GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (GameEntity projectile in _entities)
            {
                GameEntity target = _game.GetEntityWithId(projectile.projectileTargetId.Value);
                
                if (target == null)
                    continue;
                
                Vector3 direction = (target.transform.Value.position - projectile.transform.Value.position).normalized;
                direction.y = 0;
                direction = direction.normalized;
                projectile.transform.Value.Translate(direction * _commonStaticData.projectileSpeed * Time.deltaTime, Space.World);
                projectile.transform.Value.localRotation = Quaternion.LookRotation(direction);
                projectile.ReplaceTransform(projectile.transform.Value);
            }
        }
    }
}