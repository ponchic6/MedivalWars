using System;
using System.Collections.Generic;
using Code.Gameplay.Soldiers.Services;
using Code.Infrastructure.StaticData;
using Entitas;

namespace Code.Gameplay.Towers.Systems
{
    public class SoldierCreationOnMaxScoreReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly ISoldierFactory _soldierFactory;
        private readonly Random _random;

        public SoldierCreationOnMaxScoreReactiveSystem(IContext<GameEntity> context, CommonStaticData commonStaticData, ISoldierFactory soldierFactory) : base(context)
        {
            _commonStaticData = commonStaticData;
            _soldierFactory = soldierFactory;
            _random = new Random();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerScore);

        protected override bool Filter(GameEntity entity) =>
            entity.towerScore.Value > _commonStaticData.maxScore && !entity.isCatapult;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                if (entity.towerRouteIdList.Value.Count == 0)
                    continue;
                
                _soldierFactory.CreateSoldier(entity, entity.towerRouteIdList.Value[_random.Next(entity.towerRouteIdList.Value.Count)]);
            }
        }
    }
}