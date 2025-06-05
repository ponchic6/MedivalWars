using Code.Infrastructure.Systems;

namespace Code.Gameplay.Routes.Systems
{
    public class RoutesFeature : Feature
    {
        public RoutesFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<RouteDragSystem>());
            Add(systemFactory.Create<RouteDestructMarkerBySwapSystem>());
            
            Add(systemFactory.Create<RouteCreateReactiveSystem>());
            Add(systemFactory.Create<RoutePaintReactiveSystem>());
            Add(systemFactory.Create<OppositeRouteAdjustmentViewReactiveSystem>());
            Add(systemFactory.Create<RouteByDestructAdjustmentViewReactiveSystem>());
            Add(systemFactory.Create<RouteDestructByLevelDecreaseReactiveSystem>());
            Add(systemFactory.Create<RouteIdListUpdateReactiveSystem>());
            Add(systemFactory.Create<RouteMaxCountViewReactiveSystem>());
            Add(systemFactory.Create<RouteUsedCountViewReactiveSystem>());
        }
    }
}