using Code.Gameplay.Soldiers.View;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Soldiers.Registrars
{
    public class SoldierDeathViewRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private SoldierDeathView _soldierDeathView;
        
        public override void RegisterComponent() =>
            Entity.AddSoldierDeathView(_soldierDeathView);

        public override void UnregisterComponent() =>
            Entity.RemoveSoldierDeathView();
    }
}