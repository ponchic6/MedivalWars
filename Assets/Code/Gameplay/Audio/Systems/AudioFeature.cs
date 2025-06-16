using Code.Infrastructure.Systems;

namespace Code.Gameplay.Audio.Systems
{
    public class AudioFeature : Feature
    {
        public AudioFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<AudioInitializeSystem>());
            
            Add(systemFactory.Create<SoldierDeathCooldownSystem>());
            
            Add(systemFactory.Create<SoldierDestructAudioReactiveSystem>());
        }
    }
}