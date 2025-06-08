using Code.Infrastructure.Services;
using Code.Infrastructure.StaticData;
using UnityEngine;

namespace Code.Gameplay.Routes.Services
{
    public class RouteFactory : IRouteFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;

        public RouteFactory(IIdentifierService identifierService, CommonStaticData commonStaticData)
        {
            _identifierService = identifierService;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
        }
        
        public void CreateDraggingRoute(GameEntity start)
        {
            GameEntity route = _game.CreateEntity();
            route.AddId(_identifierService.Next());
            route.isRoute = true;
            route.AddViewPrefab(_commonStaticData.routePrefab);
            route.AddRouteStartId(start.id.Value);
        }

        public void CreateRoute(GameEntity start, GameEntity end)
        {
            GameEntity route = _game.CreateEntity();
            route.AddId(_identifierService.Next());
            route.isRoute = true;
            route.AddViewPrefab(_commonStaticData.routePrefab);
            route.AddRouteStartId(start.id.Value);
            route.AddRouteFinishId(end.id.Value);
            route.AddRouteDistance(Vector3.Distance(start.transform.Value.transform.position, end.transform.Value.transform.position));
            start.towerRouteIdList.Value.Add(route.id.Value);
            start.ReplaceTowerRouteIdList(start.towerRouteIdList.Value);
            start.ReplaceUsedRouteCount(start.usedRouteCount.Value + 1);
        }
    }
}