using Code.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.UI.View
{
    public class HudHandler : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _previousLevelButton;
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _defeatHudRestartLevelButton;
        [SerializeField] private Button _victoryHudNextLevelButton;
        [SerializeField] private Toggle _volumeToggle;
        [SerializeField] private GameObject _victoryHud;
        [SerializeField] private GameObject _defeatHud;
        [SerializeField] private TMP_Text _levelText;
        private GameContext _game;
        private ISaveService _saveService;

        [Inject]
        public void Construct(ISaveService saveService)
        {
            _saveService = saveService;
        }
        
        private void Awake()
        {
            _game = Contexts.sharedInstance.game;
            _playButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isPlayClick = true).AddTo(this);
            _nextLevelButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isNextLevelClick = true).AddTo(this);
            _previousLevelButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isPreviousLevelClick = true).AddTo(this);
            _restartLevelButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isMapRestartClick = true).AddTo(this);
            _defeatHudRestartLevelButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isMapRestartClick = true).AddTo(this);
            _victoryHudNextLevelButton.onClick.AsObservable().Subscribe(_ => _game.inputEntity.isNextLevelClick = true).AddTo(this);
            _volumeToggle.onValueChanged.AsObservable().Subscribe(x => _game.mainAudioEntity.isAudioOn = !x).AddTo(this);
        }

        public void SetPlayState()
        {
            _playButton.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(false);
            _previousLevelButton.gameObject.SetActive(false);
            _restartLevelButton.gameObject.SetActive(true);
            _victoryHud.gameObject.SetActive(false);
            _defeatHud.gameObject.SetActive(false);
            _levelText.transform.parent.gameObject.SetActive(false);
        }
        
        public void SetLevelChoosingState()
        {
            _nextLevelButton.interactable = _game.inputEntity.choseLevel.Value != _saveService.GetMaxLevel();
            _playButton.gameObject.SetActive(true);
            _nextLevelButton.gameObject.SetActive(true);
            _previousLevelButton.gameObject.SetActive(true);
            _victoryHud.gameObject.SetActive(false);
            _defeatHud.gameObject.SetActive(false);
            _levelText.transform.parent.gameObject.SetActive(true);
            _levelText.text = $"Уровень {_game.inputEntity.choseLevel.Value}";
        }

        public async void SetVictoryState()
        {
            await UniTask.Delay(1000);
            _playButton.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(false);
            _previousLevelButton.gameObject.SetActive(false);
            _victoryHud.gameObject.SetActive(true);
            _defeatHud.gameObject.SetActive(false);
        }

        public async void ShowDefeatHud()
        {
            await UniTask.Delay(1000);
            _playButton.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(false);
            _previousLevelButton.gameObject.SetActive(false);
            _victoryHud.gameObject.SetActive(false);
            _defeatHud.gameObject.SetActive(true);
        }
    }
}
