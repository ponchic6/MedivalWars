using System.Collections.Generic;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Towers.View
{
    public class TowerColorController : MonoBehaviour
    {
        [SerializeField] private EntityBehaviour _entityBehaviour;
        [SerializeField] private List<GameObject> _blueFractionsTower;
        [SerializeField] private List<GameObject> _redFractionsTower;
        [SerializeField] private List<GameObject> _neutralFractionsTower;
        
        public void SetFractionColor(TowerFractionsEnum fraction)
        {
            _blueFractionsTower.ForEach(x => x.SetActive(false));
            _redFractionsTower.ForEach(x => x.SetActive(false));
            _neutralFractionsTower.ForEach(x => x.SetActive(false));

            switch (fraction)
            {
                case TowerFractionsEnum.Blue:
                    _blueFractionsTower[_entityBehaviour.Entity.towerLevel.Value].SetActive(true);
                    break;
                case TowerFractionsEnum.Red:
                    _redFractionsTower[_entityBehaviour.Entity.towerLevel.Value].SetActive(true);
                    break;
                case TowerFractionsEnum.Neutral:
                    _neutralFractionsTower[_entityBehaviour.Entity.towerLevel.Value].SetActive(true);
                    break;
            }
        }
    }
}