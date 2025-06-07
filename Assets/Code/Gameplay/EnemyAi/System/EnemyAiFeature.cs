using Code.Infrastructure.Systems;

namespace Code.Gameplay.EnemyAi.System
{
    public class EnemyAiFeature : Feature
    {
        public EnemyAiFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<EnemyAiInitializeSystem>());
            
            Add(systemFactory.Create<EnemyCooldownActionSystem>());
            
            Add(systemFactory.Create<EnemyCreationRouteReactiveSystem>());
            Add(systemFactory.Create<EnemyRouteLineRendererSetterReactiveSystem>());
        }
    }
}