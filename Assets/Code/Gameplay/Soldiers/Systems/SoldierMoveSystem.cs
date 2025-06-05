using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Soldiers.Systems
{
    public class SoldierMoveSystem : IExecuteSystem
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public SoldierMoveSystem(CommonStaticData commonStaticData)
        {
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.SoldierAttackTowerId, GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                GameEntity finishTower = _game.GetEntityWithId(entity.soldierAttackTowerId.Value);
                Vector3 direction = (finishTower.transform.Value.position - entity.transform.Value.position).normalized;
                direction.y = 0;
                direction = direction.normalized;
                entity.transform.Value.Translate(direction * _commonStaticData.soldierMoveSpeed * Time.deltaTime, Space.World);
                entity.transform.Value.localRotation = Quaternion.LookRotation(direction);
                entity.ReplaceTransform(entity.transform.Value);
            }
        }
    }
}