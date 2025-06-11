using Code.Gameplay.Soldiers.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Towers.Systems
{
    public class TowerFeature : Feature
    {
        public TowerFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create <TowerLevelInitializeReactiveSystem>());
            
            Add(systemFactory.Create<TowerScorePassiveIncreasingSystem>());
            Add(systemFactory.Create<SoldierCreationSystem>());
            Add(systemFactory.Create<CatapultProjectileCreationSystem>());
            Add(systemFactory.Create<CatapultProjectileMoveSystem>());
            Add(systemFactory.Create<CatapultProjectileDestructSystem>());

            Add(systemFactory.Create<SoldierCreationOnMaxScoreReactiveSystem>());
            Add(systemFactory.Create<TowerScoreChangeViewReactiveSystem>());
            Add(systemFactory.Create<TowerLevelChangeReactiveSystem>());
            Add(systemFactory.Create<TowerLevelChangeViewReactiveSystem>());
            Add(systemFactory.Create<TowerFractionChangeViewReactiveSystem>());
            Add(systemFactory.Create<DestructRoutesOnChangeFractionReactiveSystem>());
            Add(systemFactory.Create<MaxScoreFreezeReactiveSystem>());
        }
    }
}