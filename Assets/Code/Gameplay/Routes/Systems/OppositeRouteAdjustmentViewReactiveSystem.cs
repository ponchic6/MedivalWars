using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Routes.Systems
{
    public class OppositeRouteAdjustmentViewReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;

        public OppositeRouteAdjustmentViewReactiveSystem(IContext<GameEntity> context) : base(context)
        {
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.RouteFinishId.Added());

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity newRouteEntity in entities)
            {
                int startId = newRouteEntity.routeStartId.Value;
                IGroup<GameEntity> completedRoutes = _game.GetGroup(GameMatcher.RouteFinishId);

                if (!CheckReverseRoadExist(completedRoutes, newRouteEntity, out var oppositeRoute))
                    continue;
                
                GameEntity startTowerOfNewRoute = _game.GetEntityWithId(startId);
                GameEntity startTowerOfCompletedRoute = _game.GetEntityWithId(oppositeRoute.routeStartId.Value);
                
                if (startTowerOfCompletedRoute.towerFraction.Value == startTowerOfNewRoute.towerFraction.Value)
                    oppositeRoute.isDestructed = true;
                else
                {
                    AdjustLineRenderer(oppositeRoute);
                    AdjustLineRenderer(newRouteEntity);
                }
            }
        }

        private bool CheckReverseRoadExist(IGroup<GameEntity> completedRoutes, GameEntity newRouteEntity, out GameEntity oppositeRoute)
        {
            foreach (GameEntity completedRoute in completedRoutes)
            {
                if (completedRoute.routeFinishId.Value == newRouteEntity.routeStartId.Value &&
                    completedRoute.routeStartId.Value == newRouteEntity.routeFinishId.Value)
                {
                    oppositeRoute = completedRoute;
                    return true;
                }
            }

            oppositeRoute = null;
            return false;
        } 
        
        private void AdjustLineRenderer(GameEntity route)
        {
            Vector3 startPos = route.lineRenderer.Value.GetPosition(0);
            Vector3 finishPos = route.lineRenderer.Value.GetPosition(1);
            Vector3 delta = finishPos - startPos;
            route.lineRenderer.Value.SetPosition(1, startPos + delta.normalized * route.routeDistance.Value * 0.5f);
        }
    }
}