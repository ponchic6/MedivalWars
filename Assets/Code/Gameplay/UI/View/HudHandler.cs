using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.UI.View
{
    public class HudHandler : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _previousLevelButton;
        [SerializeField] private Button _restartLevelButton;
        private GameContext _game;

        private void Awake()
        {
            _game = Contexts.sharedInstance.game;
            _playButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isPlayClick = true).AddTo(this);
            _nextLevelButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isNextLevelClick = true).AddTo(this);
            _previousLevelButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isPreviousLevelClick = true).AddTo(this);
            _restartLevelButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isMapRestartClick = true).AddTo(this);
        }

        public void SetPlayState()
        {
            _playButton.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(false);
            _previousLevelButton.gameObject.SetActive(false);
        }
        
        public void SetLevelChoosingState()
        {
            _playButton.gameObject.SetActive(true);
            _nextLevelButton.gameObject.SetActive(true);
            _previousLevelButton.gameObject.SetActive(true);
        }
    }
}
