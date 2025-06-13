using Code.Gameplay.Input.Services;
using Code.Infrastructure.Services;
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
        private readonly ICameraProvider _cameraProvider;
        private readonly IScreenTapService _screenTapService;

        public RouteDragSystem(CommonStaticData commonStaticData, ICameraProvider cameraProvider, IScreenTapService screenTapService)
        {
            _commonStaticData = commonStaticData;
            _cameraProvider = cameraProvider;
            _screenTapService = screenTapService;
            _game = Contexts.sharedInstance.game;
            _entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.RouteStartId, GameMatcher.LineRenderer).NoneOf(GameMatcher.RouteFinishId));
        }
        
        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                GameEntity startTower = _game.GetEntityWithId(entity.routeStartId.Value);
                Vector3 startPosition = startTower.transform.Value.position + Vector3.up * _commonStaticData.verticalRouteOffset;

                Vector3 endPosition;
                
                if (_game.isFinishTowerRoutesPoint) 
                    endPosition = _game.finishTowerRoutesPointEntity.transform.Value.position + Vector3.up * _commonStaticData.verticalRouteOffset;
                else
                    endPosition = GetGroundIntersect();

                entity.lineRenderer.Value.SetPosition(0, startPosition);
                entity.lineRenderer.Value.SetPosition(1, endPosition);
                
                bool hasIntersection = HasObstacleBetween(startPosition, endPosition);
                entity.isRouteIntersectingObstacle = hasIntersection;
            }
        }

        private Vector3 GetGroundIntersect()
        {
            Ray ray = _cameraProvider.GetMainCamera().ScreenPointToRay(_screenTapService.GetScreenTap);
                
            float rayLength = (0f - ray.origin.y) / ray.direction.y;
        
            if (rayLength > 0)
            {
                Vector3 hitPoint = ray.origin + ray.direction * rayLength;
                return hitPoint;
            }
        
            return Vector3.zero;
        }
        
        private bool HasObstacleBetween(Vector3 startPos, Vector3 endPos)
        {
            int obstacleLayerMask = 1 << LayerMask.NameToLayer("Obstacles");
            
            Vector3 direction = endPos - startPos;
            float distance = direction.magnitude;

            RaycastHit[] hits = Physics.RaycastAll(startPos, direction, distance, obstacleLayerMask);

            if (hits.Length == 0 || hits.Length == 1 && _game.isFinishTowerRoutesPoint)
                return false;
            
            return true;
        }
    }
}