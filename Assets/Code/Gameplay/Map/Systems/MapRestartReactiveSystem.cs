using System.Collections.Generic;
using Code.Gameplay.Map.Services;
using Entitas;

namespace Code.Gameplay.Map.Systems
{
    public class MapRestartReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly ILevelFactory _levelFactory;
        private readonly GameContext _game;

        public MapRestartReactiveSystem(IContext<GameEntity> context, ILevelFactory levelFactory) : base(context)
        {
            _levelFactory = levelFactory;
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.MapRestartClick.Added());

        protected override bool Filter(GameEntity entity) => 
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            _game.inputEntity.isMapRestartClick = false;

            _levelFactory.CleanMap();
            GameEntity hudCanvasEntity = _game.hudCanvasEntity;
            hudCanvasEntity.hudHandler.Value.SetLevelChoosingState();
            _levelFactory.SetDecorLevel(_game.choseLevel.Value);
        }
    }
}