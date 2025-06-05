using System.Collections.Generic;
using Code.Infrastructure.View;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Code.Gameplay.Towers.View
{
    public class TowerUiView : MonoBehaviour
    {
        [SerializeField] private EntityBehaviour _entityBehaviour;
        [SerializeField] private TMP_Text _textScore;
        [SerializeField] private List<Image> _usedRoutesList;
        private int _score;
        private int _maxRoutesCount;
        private int _usedRoutesCount;
        
        public int Score
        {
            set
            {
                _textScore.transform.DOKill();
                _textScore.text = value.ToString();
                _score = value;

                _textScore.transform.DOScale(1.3f, 0.15f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => {
                        _textScore.transform.DOScale(1f, 0.1f)
                            .SetEase(Ease.InQuad);
                    });
            }
            get => _score;
        }

        public void SetUsedRoutes(int value)
        {
            _usedRoutesList.ForEach(x =>
            {
                var color = x.color;
                color.a = 0.3f;
                x.color = color;
            });
            
            for (int i = 0; i < value; i++)
            {
                Color color = _usedRoutesList[i].color;
                color.a = 1f;
                _usedRoutesList[i].color = color;
            }

            _usedRoutesCount = value;
        }

        public void SetMaxRoutes(int value)
        {
            _usedRoutesList.ForEach(x => x.gameObject.SetActive(false));
                
            for (int i = 0; i < value; i++) 
                _usedRoutesList[i].gameObject.SetActive(true);
            
            SetUsedRoutes(_usedRoutesCount);
                
            _maxRoutesCount = value;
        }
    }
}