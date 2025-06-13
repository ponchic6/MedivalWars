using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.UI.View
{
    public class BreathingButton : MonoBehaviour
    {
        [SerializeField] private float scale = 1.1f;
        [SerializeField] private float duration = 1.5f;
    
        private void Start()
        {
            transform.DOScale(scale, duration / 2f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }
    }
}