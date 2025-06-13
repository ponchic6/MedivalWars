using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.View;
using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Towers.View
{
    public class TowerLevelViewController : MonoBehaviour
    {
        [SerializeField] private EntityBehaviour _entityBehaviour;
        [SerializeField] private List<Transform> _blueTowers;
        [SerializeField] private List<Transform> _redTowers;
        [SerializeField] private List<Transform> _neutralTowers;
        [SerializeField] private float _morphDuration;
        [SerializeField] private Ease _morphEase;
        [SerializeField] private Ease _bounceEase;
        private int _currentTowerIndex;
        private Coroutine _currentMorphCoroutine;

        public void StartMorph(int targetIndex)
        {
            if (targetIndex < 0 || targetIndex >= _blueTowers.Count)
                return;
            
            if (_currentMorphCoroutine != null)
            {
                StopCoroutine(_currentMorphCoroutine);
                _currentMorphCoroutine = null;
            }
            
            StopAllAnimationsAndShowTarget(targetIndex);
            
            _currentMorphCoroutine = StartCoroutine(MorphSequence(targetIndex));
        }
        
        private void StopAllAnimationsAndShowTarget(int targetIndex)
        {
            foreach (var tower in _blueTowers.Concat(_redTowers).Concat(_neutralTowers))
            {
                if (tower != null) 
                    DOTween.Kill(tower);
            }
            
            Transform targetTower = GetTowerByIndex(targetIndex);
            
            foreach (var tower in _blueTowers.Concat(_redTowers).Concat(_neutralTowers))
            {
                if (tower != null)
                {
                    tower.gameObject.SetActive(false);
                    tower.localScale = Vector3.one;
                }
            }
            
            _currentTowerIndex = targetIndex;
            
            if (targetTower != null)
            {
                targetTower.gameObject.SetActive(true);
                targetTower.localScale = Vector3.one;
            }
        }
        
        private IEnumerator MorphSequence(int targetIndex)
        {
            Transform currentTower = GetTowerByIndex(_currentTowerIndex);
            Transform targetTower = GetTowerByIndex(targetIndex);
            
            if (currentTower == null || targetTower == null)
                yield break;
            
            if (currentTower == targetTower)
            {
                currentTower.localScale = Vector3.zero;
                
                Sequence bounceSequence = DOTween.Sequence();
                bounceSequence.AppendInterval(0.1f);
                bounceSequence.Append(targetTower.DOScale(Vector3.one, _morphDuration * 0.5f)
                    .SetEase(_morphEase));
                
                yield return bounceSequence.WaitForCompletion();
            }
            else
            {
                targetTower.gameObject.SetActive(true);
                targetTower.localScale = Vector3.zero;

                Sequence morphSequence = DOTween.Sequence();

                morphSequence.Append(currentTower.DOScale(Vector3.zero, _morphDuration * 0.4f)
                    .SetEase(Ease.InQuart));

                morphSequence.AppendInterval(0.1f);

                morphSequence.AppendCallback(() => 
                {
                    currentTower.gameObject.SetActive(false);
                    currentTower.localScale = Vector3.one;
                });
                
                morphSequence.Append(targetTower.DOScale(Vector3.one, _morphDuration * 0.5f)
                    .SetEase(_morphEase));
                
                yield return morphSequence.WaitForCompletion();
                
                _currentTowerIndex = targetIndex;
            }
            
            _currentMorphCoroutine = null;
        }
        
        private Transform GetTowerByIndex(int index)
        {
            switch (_entityBehaviour.Entity.towerFraction.Value)
            {
                case TowerFractionsEnum.Blue:
                    return index < _blueTowers.Count ? _blueTowers[index] : null;
                case TowerFractionsEnum.Red:
                    return index < _redTowers.Count ? _redTowers[index] : null;
                case TowerFractionsEnum.Neutral:
                    return index < _neutralTowers.Count ? _neutralTowers[index] : null;
                default:
                    return null;
            }
        }
    }
}
