using Code.Gameplay.Towers.View;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Towers.Registrars
{
    public class TowerUiViewRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private TowerUiView towerUiView; 
        
        public override void RegisterComponent() =>
            Entity.AddTowerUiView(towerUiView);

        public override void UnregisterComponent() =>
            Entity.RemoveTowerUiView();
    }
}