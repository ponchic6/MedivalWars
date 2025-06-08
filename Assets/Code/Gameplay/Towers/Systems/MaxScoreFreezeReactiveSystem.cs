using System.Collections.Generic;
using Code.Infrastructure.StaticData;
using Entitas;

namespace Code.Gameplay.Towers.Systems
{
    public class MaxScoreFreezeReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly CommonStaticData _commonStaticData;

        public MaxScoreFreezeReactiveSystem(IContext<GameEntity> context, CommonStaticData commonStaticData) : base(context)
        {
            _commonStaticData = commonStaticData;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerScore);

        protected override bool Filter(GameEntity entity) =>
            entity.towerScore.Value > _commonStaticData.maxScore;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
                entity.towerScore.Value = _commonStaticData.maxScore;
        }
    }
}