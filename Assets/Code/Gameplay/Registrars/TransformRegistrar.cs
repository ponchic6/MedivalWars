using Code.Infrastructure.View;

namespace Code.Gameplay.Registrars
{
    public class TransformRegistrar : EntityComponentRegistrar
    {
        public override void RegisterComponent() =>
            Entity.AddTransform(transform);

        public override void UnregisterComponent() =>
            Entity.RemoveTransform();
    }
}