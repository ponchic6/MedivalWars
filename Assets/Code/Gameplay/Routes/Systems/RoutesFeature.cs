using Code.Infrastructure.Systems;

namespace Code.Gameplay.Routes.Systems
{
    public class RoutesFeature : Feature
    {
        public RoutesFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<RouteDragSystem>());
            Add(systemFactory.Create<RouteDestructMarkerOnSwapSystem>());
            
            Add(systemFactory.Create<FinishCreationRouteRS>());
            Add(systemFactory.Create<RouteFractionPaintRS>());
            Add(systemFactory.Create<RoutePaintOnObstacleIntersetcRS>());
            Add(systemFactory.Create<RouteAdjustmentOnCreateOppositeRS>());
            Add(systemFactory.Create<RouteAdjustmentOnDestructOppositeRS>());
            Add(systemFactory.Create<ChangeMaxRoutesOnLevelChangeRS>());
            Add(systemFactory.Create<ChangeRouteIdListOnDestructRouteRS>());
            Add(systemFactory.Create<UsedCountRouteChangeViewRS>());
            Add(systemFactory.Create<MaxCountRouteChangeViewRS>());
        }
    }
}