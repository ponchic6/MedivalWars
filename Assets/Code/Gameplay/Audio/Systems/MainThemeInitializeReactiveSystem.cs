using System.Collections.Generic;
using Code.Infrastructure.StaticData;
using Entitas;

namespace Code.Gameplay.Audio.Systems
{
    public class MainThemeInitializeReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly CommonStaticData _commonStaticData;

        public MainThemeInitializeReactiveSystem(IContext<GameEntity> context, CommonStaticData commonStaticData) : base(context)
        {
            _commonStaticData = commonStaticData;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.AudioSource.Added());

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities) 
                entity.audioSource.Value.Play(_commonStaticData.mainTheme, true);
        }
    }
}