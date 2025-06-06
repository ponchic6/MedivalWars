using System.Collections.Generic;
using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Routes.Systems
{
    public class RouteCreateReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;
        
        public RouteCreateReactiveSystem(IContext<GameEntity> context, CommonStaticData commonStaticData) : base(context)
        {
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.LeftMouseButtonHold.Removed());

        protected override bool Filter(GameEntity entity) =>
            true;

        protected override void Execute(List<GameEntity> entities)
        {
            var routesWithoutFinish = _game.GetGroup(GameMatcher.AllOf(GameMatcher.RouteStartId).NoneOf(GameMatcher.RouteFinishId));

            if (!_game.isStartTowerRoutesPoint)
                return;
            
            if (!_game.isFinishTowerRoutesPoint)
            {
                RejectRouteBuilding(_game.startTowerRoutesPointEntity, routesWithoutFinish);
                return;
            }

            GameEntity startTower = _game.startTowerRoutesPointEntity;
            GameEntity finishTower = _game.finishTowerRoutesPointEntity;

            startTower.isStartTowerRoutesPoint = false;
            finishTower.isFinishTowerRoutesPoint = false;

            if (CheckExistingRoute(startTower, finishTower) || startTower.usedRouteCount.Value >= startTower.maxRouteCount.Value)
            {
                RejectRouteBuilding(startTower, routesWithoutFinish);
                return;
            }

            FinishRoute(routesWithoutFinish, startTower, finishTower);
        }

        private void FinishRoute(IGroup<GameEntity> routesWithoutFinish, GameEntity start, GameEntity finish)
        {
            foreach (GameEntity entity in routesWithoutFinish.GetEntities())
            {
                start.towerRouteIdList.Value.Add(entity.id.Value);
                start.ReplaceTowerRouteIdList(start.towerRouteIdList.Value);
                start.ReplaceUsedRouteCount(start.usedRouteCount.Value + 1);
                entity.AddRouteFinishId(finish.id.Value);
                entity.lineRenderer.Value.SetPosition(1, finish.transform.Value.position + Vector3.up * _commonStaticData.verticalRouteOffset);
                entity.AddRouteDistance(Vector3.Distance(start.transform.Value.transform.position, finish.transform.Value.transform.position));
                AddRouteCollider(entity);
            }
        }

        private void RejectRouteBuilding(GameEntity startPoint, IGroup<GameEntity> routesWithoutFinish)
        {
            startPoint.isStartTowerRoutesPoint = false;
            
            foreach (GameEntity routeEntity in routesWithoutFinish) 
                routeEntity.isDestructed = true;
        }

        private bool CheckExistingRoute(GameEntity start, GameEntity finish)
        {
            foreach (GameEntity routeEntity in _game.GetGroup(GameMatcher.AllOf(GameMatcher.RouteStartId, GameMatcher.RouteFinishId)))
            {
                if (routeEntity.routeStartId.Value == start.id.Value && 
                    routeEntity.routeFinishId.Value == finish.id.Value)
                    return true;
            }

            return false;
        }

        private void AddRouteCollider(GameEntity entity)
        {
            LineRenderer line = entity.lineRenderer.Value;
            Vector3 startCollider = line.GetPosition(0);
            Vector3 endCollider = line.GetPosition(1);
            Vector3 center = (startCollider + endCollider) / 2f;
            float length = Vector3.Distance(startCollider, endCollider);
            GameObject colliderObj = new GameObject("LineCollider");
            colliderObj.transform.SetParent(line.transform);
            colliderObj.transform.position = center;
            colliderObj.transform.rotation = Quaternion.LookRotation((endCollider - startCollider).normalized);
            BoxCollider box = colliderObj.AddComponent<BoxCollider>();
            box.size = new Vector3(0.03f, 0.03f, length);
        }
    }
}