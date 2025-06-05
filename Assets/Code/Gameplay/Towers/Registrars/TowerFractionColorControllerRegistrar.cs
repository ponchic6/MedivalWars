using Code.Gameplay.Towers.View;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Towers.Registrars
{
    public class TowerFractionColorControllerRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private TowerColorController _towerColorController; 
        
        public override void RegisterComponent() =>
            Entity.AddTowerFractionColorController(_towerColorController);

        public override void UnregisterComponent() =>
            Entity.RemoveTowerFractionColorController();
    }
}