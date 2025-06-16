using System.Collections.Generic;
using Code.Gameplay.Map.Services;
using Entitas;

namespace Code.Gameplay.UI.Systems
{
    public class StartLevelReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly ILevelFactory _levelFactory;

        public StartLevelReactiveSystem(IContext<GameEntity> context, ILevelFactory levelFactory) : base(context)
        {
            _game = Contexts.sharedInstance.game;
            _levelFactory = levelFactory;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.PlayClick.Added(), GameMatcher.MapRestartClick.Added());

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            _game.inputEntity.isPlayClick = false;
            _game.inputEntity.isMapRestartClick = false;
            
            _levelFactory.CleanMap();
            GameEntity hudCanvasEntity = _game.hudCanvasEntity;
            hudCanvasEntity.hudHandler.Value.SetPlayState();
            _levelFactory.StartLevel(_game.choseLevel.Value);
        }
    }
}