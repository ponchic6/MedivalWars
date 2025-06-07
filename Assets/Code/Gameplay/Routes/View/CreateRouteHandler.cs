using Code.Gameplay.Routes.Services;
using Code.Gameplay.Towers;
using Code.Gameplay.Towers.View;
using Code.Infrastructure.View;
using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.Gameplay.Routes.View
{
    public class CreateRouteHandler : MonoBehaviour
    {
        [SerializeField] private PointerHandler _pointerHandler;
        [SerializeField] private EntityBehaviour _entityBehaviour;
        private GameContext _game;
        private IRouteFactory _routeFactory;

        [Inject]
        public void Construct(IRouteFactory routeFactory)
        {
            _routeFactory = routeFactory;
        }

        private void Awake()
        {
            _game = Contexts.sharedInstance.game;

            _pointerHandler.OnPointerDownEvent += MarkAsStartPoint;
            _pointerHandler.OnPointerEnterEvent += MarkAsFinishPoint;
            _pointerHandler.OnPointerExitEvent += RemoveFinishMark;
        }

        private void MarkAsStartPoint(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            
            if (_entityBehaviour.Entity.towerFraction.Value != TowerFractionsEnum.Blue)
                return;
            
            _entityBehaviour.Entity.isStartTowerRoutesPoint = true;
            _routeFactory.CreateDraggingRoute(_entityBehaviour.Entity);
        }

        private void MarkAsFinishPoint(PointerEventData eventData)
        {
            if (_entityBehaviour.Entity.isStartTowerRoutesPoint)
                return;

            if (!_game.isStartTowerRoutesPoint)
                return;
            
            _entityBehaviour.Entity.isFinishTowerRoutesPoint = true;
        }

        private void RemoveFinishMark(PointerEventData obj) =>
            _entityBehaviour.Entity.isFinishTowerRoutesPoint = false;
    }
}