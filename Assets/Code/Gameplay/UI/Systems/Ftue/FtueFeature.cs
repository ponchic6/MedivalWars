using Code.Infrastructure.Systems;

namespace Code.Gameplay.UI.Systems.Ftue
{
    public class FtueFeature : Feature
    {
        public FtueFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<CursorFtueStartCreationRouteReactiveSystem>());
            Add(systemFactory.Create<CursorFtueStartDestructRouteReactiveSystem>());
            Add(systemFactory.Create<CursorFtueFinishTextReactiveSystem>());
            Add(systemFactory.Create<FtueCompleteReactiveSystem>());
        }
    }
}