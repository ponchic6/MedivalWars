using Code.Gameplay.Input.Services;
using Code.Gameplay.Towers;
using Code.Infrastructure.Services;
using Code.Infrastructure.View;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Routes.Systems
{
    public class RouteDestructMarkerOnSwapSystem : IExecuteSystem
    {
        private readonly ICameraProvider _cameraProvider;
        private readonly IScreenTapService _screenTapService;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public RouteDestructMarkerOnSwapSystem(ICameraProvider cameraProvider, IScreenTapService screenTapService)
        {
            _cameraProvider = cameraProvider;
            _screenTapService = screenTapService;
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.TapHold);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                IGroup<GameEntity> notCompletedRoutes = _game.GetGroup(GameMatcher.AllOf(GameMatcher.RouteStartId).NoneOf(GameMatcher.RouteFinishId));
       
                if (notCompletedRoutes.count != 0)
                    continue;

                if (!Intersected(out var hits))
                    continue;

                foreach (RaycastHit hit in hits)
                {
                    if (!hit.collider.GetComponentInParent<LineRenderer>())
                        continue;
           
                    EntityBehaviour entityBehaviour = hit.collider.GetComponentInParent<EntityBehaviour>();
                    if (!entityBehaviour) 
                        continue;
           
                    if (!entityBehaviour.Entity.isRoute)
                        continue;

                    GameEntity startTowerEntity = _game.GetEntityWithId(entityBehaviour.Entity.routeStartId.Value);
           
                    if (startTowerEntity.towerFraction.Value == TowerFractionsEnum.Blue)
                    {
                        entityBehaviour.Entity.isDestructed = true;
                        return;
                    }
                }
            }
        }

        private bool Intersected(out RaycastHit[] hits)
        {
            Ray ray = _cameraProvider.GetMainCamera().ScreenPointToRay(_screenTapService.GetScreenTap);

            hits = Physics.RaycastAll(ray);
       
            return hits.Length != 0;
        }
    }
}