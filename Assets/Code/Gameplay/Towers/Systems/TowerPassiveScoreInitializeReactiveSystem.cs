using System.Collections.Generic;
using Code.Infrastructure.StaticData;
using Entitas;

namespace Code.Gameplay.Towers.Systems
{
    public class TowerPassiveScoreInitializeReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly CommonStaticData _commonStaticData;

        public TowerPassiveScoreInitializeReactiveSystem(IContext<GameEntity> context, CommonStaticData commonStaticData) : base(context)
        {
            _commonStaticData = commonStaticData;
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerLevel.Added());

        protected override bool Filter(GameEntity entity) =>
            !entity.hasTowerScoreIncreasingCooldown && !entity.isDecor;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities) 
                AddIncreasingCooldown(entity);
        }
        
        private void AddIncreasingCooldown(GameEntity entity)
        {
            switch (entity.towerLevel.Value)
            {
                case 0:
                    entity.AddTowerScoreIncreasingCooldown(_commonStaticData.towerScoreIncreasingCooldownZeroLevel);
                    break; 
                case 1:
                    entity.AddTowerScoreIncreasingCooldown(_commonStaticData.towerScoreIncreasingCooldownFirstLevel);
                    break;
                case 2:
                    entity.AddTowerScoreIncreasingCooldown(_commonStaticData.towerScoreIncreasingCooldownSecondLevel);
                    break;
            }
        }

    }
}