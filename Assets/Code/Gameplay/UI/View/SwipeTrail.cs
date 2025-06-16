using Code.Gameplay.Input.Services;
using Code.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.UI.View
{
    public class SwipeTrail : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _trailDistance;
        
        private ICameraProvider _cameraProvider;
        private IScreenTapService _screenTapService;
        private GameContext _game;

        [Inject]
        public void Construct(ICameraProvider cameraProvider, IScreenTapService screenTapService)
        {
            _screenTapService = screenTapService;
            _cameraProvider = cameraProvider;
            _game = Contexts.sharedInstance.game;
        }

        private void Update()
        {
            Vector3 worldPos = ScreenToWorldPosition(UnityEngine.Input.mousePosition);
            _trail.transform.position = worldPos;

            if (!_screenTapService.IsTapping || _game.isStartTowerRoutesPoint)
            {
                _trail.enabled = false;
                return;
            }

            _trail.enabled = true;
        }

        Vector3 ScreenToWorldPosition(Vector2 screenPos)
        {
            Camera mainCamera = _cameraProvider.GetMainCamera();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, _trailDistance));
        
            return worldPos;
        }
    
    }
}
