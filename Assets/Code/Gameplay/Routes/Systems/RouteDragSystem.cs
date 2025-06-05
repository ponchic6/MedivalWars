using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Routes.Systems
{
    public class RouteDragSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;
        private readonly CommonStaticData _commonStaticData;

        public RouteDragSystem(CommonStaticData commonStaticData)
        {
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
            _entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.RouteStartId, GameMatcher.LineRenderer).NoneOf(GameMatcher.RouteFinishId));
        }
        
        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                GameEntity startTower = _game.GetEntityWithId(entity.routeStartId.Value);
                entity.lineRenderer.Value.SetPosition(0, startTower.transform.Value.position + Vector3.up * _commonStaticData.verticalRouteOffset);
                Vector3 groundIntersect = GetGroundIntersect();
                entity.lineRenderer.Value.SetPosition(1, groundIntersect);
            }
        }

        private Vector3 GetGroundIntersect()
        {
            Camera playerCamera = Camera.main;
            Vector3 mousePosition = UnityEngine.Input.mousePosition;
            Ray ray = playerCamera.ScreenPointToRay(mousePosition);
                
            float rayLength = (0f - ray.origin.y) / ray.direction.y;
        
            if (rayLength > 0)
            {
                Vector3 hitPoint = ray.origin + ray.direction * rayLength;
                return hitPoint;
            }
        
            return Vector3.zero;
        }
    }
}