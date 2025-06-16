using Code.Gameplay.Audio.Systems;
using Code.Gameplay.EnemyAi.System;
using Code.Gameplay.Input.Systems;
using Code.Gameplay.Map.Systems;
using Code.Gameplay.Routes.Systems;
using Code.Gameplay.Soldiers.Systems;
using Code.Gameplay.Towers.Systems;
using Code.Gameplay.UI.Systems;
using Code.Gameplay.UI.Systems.Ftue;
using Code.Infrastructure.Destroy;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;

namespace Code.Gameplay
{
    public class MainFeature : Feature
    {
        public MainFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<ViewFeature>());
            Add(systemFactory.Create<InputFeature>());
            Add(systemFactory.Create<UiFeature>());
            Add(systemFactory.Create<FtueFeature>());
            Add(systemFactory.Create<MapFeature>());
            Add(systemFactory.Create<EnemyAiFeature>());
            Add(systemFactory.Create<TowerFeature>());
            Add(systemFactory.Create<RoutesFeature>());
            Add(systemFactory.Create<SoldiersFeature>());
            Add(systemFactory.Create<AudioFeature>());
            Add(systemFactory.Create<DestroyFeature>());
        }
    }
}