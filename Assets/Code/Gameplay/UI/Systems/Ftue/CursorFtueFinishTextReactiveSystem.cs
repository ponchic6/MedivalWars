using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.UI.Systems.Ftue
{
    public class CursorFtueFinishTextReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;

        public CursorFtueFinishTextReactiveSystem(IContext<GameEntity> context) : base(context)
        {
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.RouteFinishId.Removed());

        protected override bool Filter(GameEntity entity) =>
            _game.inputEntity.choseLevel.Value == 0;

        protected override void Execute(List<GameEntity> entities)
        {
            _game.hudCanvasEntity.cursorFtue.Value.StopRouteDestructDrag();
            _game.hudCanvasEntity.cursorFtue.Value.ShowFinalText();
        }
    }
}