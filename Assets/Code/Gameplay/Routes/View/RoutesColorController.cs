using Code.Gameplay.Towers;
using UnityEngine;

namespace Code.Gameplay.Routes.View
{
    public class RoutesColorController : MonoBehaviour
    {
        [SerializeField] private Color _blueFraction;
        [SerializeField] private Color _redFraction;
        [SerializeField] private Color _intersectingColor;
        [SerializeField] private LineRenderer _lineRenderer;

        public void SetFractionColor(TowerFractionsEnum fraction)
        {
            Color color = fraction switch
            {
                TowerFractionsEnum.Blue => _blueFraction,
                TowerFractionsEnum.Red => _redFraction
            };
            
            _lineRenderer.material.SetColor("_Color", color);
        }

        public void SetIntersectingColor() =>
            _lineRenderer.material.SetColor("_Color", _intersectingColor);
    }
}