using Code.Gameplay.Routes.View;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Routes.Registrar
{
    public class RoutesColorControllerRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private RoutesColorController _routesColorController;
        
        public override void RegisterComponent() =>
            Entity.AddRoutesColorController(_routesColorController);

        public override void UnregisterComponent() =>
            Entity.RemoveRoutesColorController();
    }
}