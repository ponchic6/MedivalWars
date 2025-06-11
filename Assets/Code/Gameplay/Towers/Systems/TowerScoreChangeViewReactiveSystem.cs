using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Towers.Systems
{
    public class TowerScoreChangeViewReactiveSystem : ReactiveSystem<GameEntity>
    {
        public TowerScoreChangeViewReactiveSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerScore);

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities) 
                entity.towerUiView.Value.Score = entity.towerScore.Value;
        }
    }
}