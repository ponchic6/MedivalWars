using System.Collections.Generic;
using Code.Gameplay.Audio.View;
using Code.Gameplay.Towers;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Audio.Systems
{
    public class AudioVolumeReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;

        public AudioVolumeReactiveSystem(IContext<GameEntity> context) : base(context)
        {
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.AudioOff.AddedOrRemoved());

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            AudioSourceController audioSourceController = _game.mainAudioEntity.audioSource.Value;
            audioSourceController.SetVolume(_game.mainAudioEntity.isAudioOff ? 0 : 1);
        }
    }
}