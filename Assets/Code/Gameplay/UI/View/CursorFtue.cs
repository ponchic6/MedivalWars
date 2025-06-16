using System.Collections;
using Code.Infrastructure.Services;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.UI.View
{
    public class CursorFtue : MonoBehaviour
    {
        [SerializeField] private RectTransform _cursorRectTransform;
        [SerializeField] private TMP_Text _ftueRouteCreationText;
        [SerializeField] private TMP_Text _ftueRouteDestructionText;
        [SerializeField] private TMP_Text _ftueFinalText;
        [SerializeField] private Canvas _canvas;

        private ICameraProvider _cameraProvider;
        private Coroutine _coroutine;
        private bool _isAnimating;
        private bool _firstStepFtueCompleted;
        private bool _secondStepFtueCompleted;
        private bool _thirdStepFtueCompleted;

        [Inject]
        public void Construct(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }
        
        public void StartRouteCreationDrag(Vector3 startWorldPos, Vector3 endWorldPos)
        {
            if (_isAnimating || _firstStepFtueCompleted)
                return;

            _ftueRouteCreationText.gameObject.SetActive(true);
            _coroutine = StartCoroutine(CursorAnimationDrag(startWorldPos, endWorldPos));
        }

        public void StopRouteCreationDrag()
        {
            StopCoroutine(_coroutine);
            _cursorRectTransform.DOKill();
            _isAnimating = false;
            _cursorRectTransform.gameObject.SetActive(false);
            _ftueRouteCreationText.gameObject.SetActive(false);
            _firstStepFtueCompleted = true;
        }

        public void StartRouteDestructDrag(Vector3 startWorldPos, Vector3 endWorldPos)
        {
            if (_isAnimating || _secondStepFtueCompleted)
                return;

            _ftueRouteDestructionText.gameObject.SetActive(true);
            _coroutine = StartCoroutine(CursorAnimationDrag(new Vector3(endWorldPos.x, 0, startWorldPos.z), new Vector3(startWorldPos.x, 0, endWorldPos.z)));
        }
        
        public void StopRouteDestructDrag()
        {
            StopCoroutine(_coroutine);
            _cursorRectTransform.DOKill();
            _isAnimating = false;
            _cursorRectTransform.gameObject.SetActive(false);
            _ftueRouteDestructionText.gameObject.SetActive(false);
            _ftueFinalText.gameObject.SetActive(true);
            _secondStepFtueCompleted = true;
        }

        public void CompleteFtue()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _ftueRouteCreationText.gameObject.SetActive(false);
            _ftueRouteDestructionText.gameObject.SetActive(false);
            _ftueFinalText.gameObject.SetActive(false);
            _firstStepFtueCompleted = true;
            _secondStepFtueCompleted = true;
            _thirdStepFtueCompleted = true;
            _cursorRectTransform.gameObject.SetActive(false);
        }

        private IEnumerator CursorAnimationDrag(Vector3 startWorldPos, Vector3 endWorldPos)
        {
            _isAnimating = true;
        
            while (_isAnimating)
            {
                Vector2 startScreenPos = _cameraProvider.GetMainCamera().WorldToScreenPoint(startWorldPos);
                Vector2 endScreenPos = _cameraProvider.GetMainCamera().WorldToScreenPoint(endWorldPos);
            
                Vector2 startCanvasPos = ScreenToCanvasPosition(startScreenPos);
                Vector2 endCanvasPos = ScreenToCanvasPosition(endScreenPos);
            
                _cursorRectTransform.gameObject.SetActive(true);
                _cursorRectTransform.anchoredPosition = startCanvasPos;
                _cursorRectTransform.localScale = Vector3.one;
            
                yield return new WaitForSeconds(0.5f);
            
                _cursorRectTransform.DOScale(0.6f, 0.1f);
                yield return new WaitForSeconds(0.1f);

                float moveTime = 1f;
                _cursorRectTransform.DOAnchorPos(endCanvasPos, moveTime).SetEase(Ease.InOutQuad);
                yield return new WaitForSeconds(moveTime);
                _cursorRectTransform.DOScale(1f, 0.1f);
                yield return new WaitForSeconds(0.6f);
            }
        
            _cursorRectTransform.gameObject.SetActive(false);
            _isAnimating = false;
        }

        private Vector2 ScreenToCanvasPosition(Vector2 screenPosition)
        {
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                screenPosition,
                _canvas.worldCamera,
                out canvasPosition
            );
            return canvasPosition;
        }
    }
}
