using Entitas;
using UnityEngine;

namespace Code.Gameplay.Audio.Systems
{
    public class SoldierDeathCooldownSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public SoldierDeathCooldownSystem()
        {
            _game = Contexts.sharedInstance.game;
        }

        public void Execute()
        {
            GameEntity audioEntity = _game.mainAudioEntity;
            
            if (audioEntity.soldierDeathAudioCooldown.Value > 0)
                audioEntity.soldierDeathAudioCooldown.Value -= Time.deltaTime;
        }
    }
}