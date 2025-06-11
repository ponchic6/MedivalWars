using System.Collections.Generic;
using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Towers.Systems
{
    public class TowerLevelChangeReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly CommonStaticData _commonStaticData;

        public TowerLevelChangeReactiveSystem(IContext<GameEntity> context, CommonStaticData commonStaticData) : base(context)
        {
            _commonStaticData = commonStaticData;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerScore);

        protected override bool Filter(GameEntity entity) =>
            entity.hasTowerLevel;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                if (entity.towerScore.Value <= _commonStaticData.scoreFromZeroToFirstLevel && entity.towerLevel.Value != 0) 
                    entity.ReplaceTowerLevel(0);

                if (entity.towerScore.Value > _commonStaticData.scoreFromZeroToFirstLevel && entity.towerScore.Value <= _commonStaticData.scoreFromFirstToSecondLevel && entity.towerLevel.Value != 1) 
                    entity.ReplaceTowerLevel(1);

                if (entity.towerScore.Value > _commonStaticData.scoreFromFirstToSecondLevel && entity.towerLevel.Value != 2)
                    entity.ReplaceTowerLevel(2);
            }
        }
    }
}