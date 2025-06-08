using System;
using System.Collections.Generic;
using Code.Infrastructure.StaticData;
using Code.Infrastructure.View;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Towers.View
{
    public class TowerUiView : MonoBehaviour
    {
        [SerializeField] private EntityBehaviour _entityBehaviour;
        [SerializeField] private TMP_Text _textScore;
        [SerializeField] private TMP_Text _maxTextScore;
        [SerializeField] private List<Image> _usedRoutesList;
        private int _usedRoutesCount;
        private CommonStaticData _commonStaticData;

        [Inject]
        public void Construct(CommonStaticData commonStaticData)
        {
            _commonStaticData = commonStaticData;
        }
        
        public int Score
        {
            set
            {
                if (value >= _commonStaticData.maxScore)
                {
                    _textScore.gameObject.SetActive(false);
                    _maxTextScore.gameObject.SetActive(true);
                    _maxTextScore.transform.DOKill();
                    _maxTextScore.transform.DOScale(1.3f, 0.15f)
                        .SetEase(Ease.OutQuad)
                        .OnComplete(() => {
                            _maxTextScore.transform.DOScale(1f, 0.1f)
                                .SetEase(Ease.InQuad);
                        });
                    return;
                }
                
                _maxTextScore.gameObject.SetActive(false);
                _textScore.gameObject.SetActive(true);
                _textScore.transform.DOKill();
                _textScore.text = value.ToString();

                _textScore.transform.DOScale(1.3f, 0.15f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => {
                        _textScore.transform.DOScale(1f, 0.1f)
                            .SetEase(Ease.InQuad);
                    });
            }
        }
        
        public void SetUsedRoutes(int value)
        {
            if (_entityBehaviour.Entity.isCatapult)
                return;
            
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
            if (_entityBehaviour.Entity.isCatapult)
                return;
            
            _usedRoutesList.ForEach(x => x.gameObject.SetActive(false));
                
            for (int i = 0; i < value; i++) 
                _usedRoutesList[i].gameObject.SetActive(true);
            
            SetUsedRoutes(_usedRoutesCount);
        }
    }
}