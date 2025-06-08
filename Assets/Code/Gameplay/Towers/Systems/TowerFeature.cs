using Code.Gameplay.Soldiers.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Towers.Systems
{
    public class TowerFeature : Feature
    {
        public TowerFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create <TowerLevelInitializeSystem>());
            
            Add(systemFactory.Create<TowerScorePassiveIncreasingSystem>());
            Add(systemFactory.Create<TowerSoldierCreationCooldownSystem>());
            Add(systemFactory.Create<CatapultShootingCooldownSystem>());
            Add(systemFactory.Create<CatapultShootingSystem>());
            Add(systemFactory.Create<ProjectileMoveSystem>());
            Add(systemFactory.Create<ProjectileDestructSystem>());

            Add(systemFactory.Create<SoldierCreationByMaxScoreReactiveSystem>());
            Add(systemFactory.Create<TowerScoreViewReactiveSystem>());
            Add(systemFactory.Create<TowerLevelReactiveSystem>());
            Add(systemFactory.Create<TowerLevelViewReactiveSystem>());
            Add(systemFactory.Create<TowerFractionChangeReactiveSystem>());
            Add(systemFactory.Create<MaxScoreFreezeReactiveSystem>());
        }
    }
}