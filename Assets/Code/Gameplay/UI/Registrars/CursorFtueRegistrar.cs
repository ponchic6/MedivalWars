using Code.Gameplay.UI.View;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.UI.Registrars
{
    public class CursorFtueRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private CursorFtue _cursorFtue;
        
        public override void RegisterComponent() =>
            Entity.AddCursorFtue(_cursorFtue);

        public override void UnregisterComponent() =>
            Entity.RemoveCursorFtue();
    }
}