using Code.Gameplay.Map.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.UI.Systems
{
    public class UiFeature : Feature
    {
        public UiFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<CanvasInitializeSystem>());
            
            Add(systemFactory.Create<MapRestartReactiveSystem>());
            Add(systemFactory.Create<StartLevelReactiveSystem>());
            Add(systemFactory.Create<LevelChangeUiReactiveSystem>());
        }
    }
}