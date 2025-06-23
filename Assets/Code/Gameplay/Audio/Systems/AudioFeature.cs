using Code.Infrastructure.Systems;

namespace Code.Gameplay.Audio.Systems
{
    public class AudioFeature : Feature
    {
        public AudioFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<AudioInitializeSystem>());
            
            Add(systemFactory.Create<SoldierDeathAudioCooldownSystem>());
            
            Add(systemFactory.Create<MainThemeInitializeReactiveSystem>());
            Add(systemFactory.Create<SoldierDestructAudioReactiveSystem>());
            Add(systemFactory.Create<AudioVolumeReactiveSystem>());
        }
    }
}