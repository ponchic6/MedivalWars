using Code.Infrastructure.Systems;

namespace Code.Gameplay.Soldiers.Systems
{
    public class SoldiersFeature : Feature
    {
        public SoldiersFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<SoldierCreateSystem>());
            Add(systemFactory.Create<SoldierMoveSystem>());
        }
    }
}