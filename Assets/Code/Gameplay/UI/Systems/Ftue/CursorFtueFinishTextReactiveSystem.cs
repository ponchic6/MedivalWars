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
            context.CreateCollector(GameMatcher.Route.Removed());

        protected override bool Filter(GameEntity entity)
        {
            IGroup<GameEntity> routes = _game.GetGroup(GameMatcher.Route);
            int routesCount = routes.count;
            return routesCount == 0 && _game.inputEntity.choseLevel.Value == 0;
        }

        protected override void Execute(List<GameEntity> entities) =>
            _game.hudCanvasEntity.cursorFtue.Value.StopRouteDestructDrag();
    }
}