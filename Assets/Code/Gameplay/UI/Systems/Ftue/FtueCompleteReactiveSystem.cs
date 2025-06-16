using System.Collections.Generic;
using Code.Gameplay.Towers;
using Entitas;

namespace Code.Gameplay.UI.Systems.Ftue
{
    public class FtueCompleteReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;

        public FtueCompleteReactiveSystem(IContext<GameEntity> context) : base(context)
        {
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => 
            context.CreateCollector(GameMatcher.TowerFraction);

        protected override bool Filter(GameEntity entity) =>
            entity.isTower && _game.inputEntity.choseLevel.Value == 0;

        protected override void Execute(List<GameEntity> entities)
        {
            IGroup<GameEntity> towerFractionEntities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.TowerFraction, GameMatcher.Tower));

            foreach (GameEntity entity in towerFractionEntities)
            {
                if (entity.towerFraction.Value != TowerFractionsEnum.Blue)
                    return;
            }

            _game.hudCanvasEntity.cursorFtue.Value.CompleteFtue();
        }
    }
}