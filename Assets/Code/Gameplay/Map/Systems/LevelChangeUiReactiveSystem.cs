using System.Collections.Generic;
using Code.Gameplay.Map.Services;
using Entitas;
using TMPro;

namespace Code.Gameplay.Map.Systems
{
    public class LevelChangeUiReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly ILevelFactory _levelFactory;

        public LevelChangeUiReactiveSystem(IContext<GameEntity> context, ILevelFactory levelFactory) : base(context)
        {
            _levelFactory = levelFactory;
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.NextLevelClick.Added(), GameMatcher.PreviousLevelClick.Added());

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            if (_game.inputEntity.isNextLevelClick)
            {
                if (_game.inputEntity.choseLevel.Value >= 9)
                    _game.ReplaceChoseLevel(9);
                else
                    _game.ReplaceChoseLevel(_game.choseLevel.Value + 1);
            }
            else if (_game.inputEntity.isPreviousLevelClick)
            {
                if (_game.inputEntity.choseLevel.Value <= 0)
                    _game.ReplaceChoseLevel(0);
                else
                    _game.ReplaceChoseLevel(_game.choseLevel.Value - 1);
            }
            
            _game.inputEntity.isNextLevelClick = false;
            _game.inputEntity.isPreviousLevelClick = false;
            
            _levelFactory.CleanMap();
            _levelFactory.SetDecorLevel(_game.choseLevel.Value);
        }
    }
}