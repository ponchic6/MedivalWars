using Code.Gameplay.Towers.View;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Towers.Registrars
{
    public class TowerLevelViewControllerRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private TowerLevelViewController _towerLevelViewController; 
        
        public override void RegisterComponent() =>
            Entity.AddTowerLevelViewController(_towerLevelViewController);

        public override void UnregisterComponent() =>
            Entity.RemoveTowerLevelViewController();
    }
}