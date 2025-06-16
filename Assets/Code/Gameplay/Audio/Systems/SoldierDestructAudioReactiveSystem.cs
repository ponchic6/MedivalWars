using System.Collections.Generic;
using Code.Gameplay.Audio.View;
using Code.Gameplay.Towers;
using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Audio.Systems
{
    public class SoldierDestructAudioReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;

        public SoldierDestructAudioReactiveSystem(IContext<GameEntity> context, CommonStaticData commonStaticData) : base(context)
        {
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.SoldierHealth);

        protected override bool Filter(GameEntity entity) =>
            entity.soldierHealth.Value <= 0 && _game.isMainAudio;

        protected override void Execute(List<GameEntity> entities)
        {
            if (_game.mainAudioEntity.soldierDeathAudioCooldown.Value > 0)
                return;
            
            foreach (GameEntity entity in entities)
            {
                AudioSourceController audioSourceController = _game.mainAudioEntity.audioSource.Value;
                
                if (entity.towerFraction.Value == TowerFractionsEnum.Blue) 
                    audioSourceController.Play(_commonStaticData.blueSoldierDestructSound);
                
                if (entity.towerFraction.Value == TowerFractionsEnum.Red)
                    audioSourceController.Play(_commonStaticData.redSoldierDestructSound);
                
                _game.mainAudioEntity.ReplaceSoldierDeathAudioCooldown(_commonStaticData.soldierDestructAudioCooldown);
                break;
            }
        }
    }
}