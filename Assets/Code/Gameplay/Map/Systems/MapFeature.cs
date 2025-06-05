using Code.Infrastructure.Systems;

namespace Code.Gameplay.Map.Systems
{
    public class MapFeature : Feature
    {
        public MapFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<MapInitializeSystem>());
        }
    }
}