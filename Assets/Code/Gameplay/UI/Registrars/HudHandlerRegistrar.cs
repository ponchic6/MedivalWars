using Code.Gameplay.UI.View;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.UI.Registrars
{
    public class HudHandlerRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private HudHandler _hudHandler;
        
        public override void RegisterComponent() =>
            Entity.AddHudHandler(_hudHandler);

        public override void UnregisterComponent() =>
            Entity.RemoveHudHandler();
    }
}