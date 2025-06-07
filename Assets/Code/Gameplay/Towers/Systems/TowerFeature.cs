using Code.Gameplay.Soldiers.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Towers.Systems
{
    public class TowerFeature : Feature
    {
        public TowerFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create <TowerLevelInitializeSystem>());
            
            Add(systemFactory.Create<TowerScoreIncreasingSystem>());
            Add(systemFactory.Create<TowerSoldierCreationCooldownSystem>());

            Add(systemFactory.Create<TowerScoreViewReactiveSystem>());
            Add(systemFactory.Create<TowerLevelReactiveSystem>());
            Add(systemFactory.Create<TowerLevelViewReactiveSystem>());
            Add(systemFactory.Create<TowerFractionChangeReactiveSystem>());
        }
    }
}