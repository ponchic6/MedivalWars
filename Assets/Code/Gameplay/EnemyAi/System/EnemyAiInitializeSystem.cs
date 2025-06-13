using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.EnemyAi.System
{
    public class EnemyAiInitializeSystem : IInitializeSystem
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;

        public EnemyAiInitializeSystem(CommonStaticData commonStaticData)
        {
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
        }
        
        public void Initialize()
        {
            GameEntity enemyAction = _game.CreateEntity();
            enemyAction.AddEnemyActionCooldown(_commonStaticData.fromEnemyActionCooldown + Random.value * _commonStaticData.toEnemyActionCooldown);
        }
    }
}