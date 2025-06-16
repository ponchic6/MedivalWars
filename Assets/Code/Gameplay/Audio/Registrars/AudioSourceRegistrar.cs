using Code.Gameplay.Audio.View;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Audio.Registrars
{
    public class AudioSourceRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private AudioSourceController _audioSource;
        
        public override void RegisterComponent() =>
            Entity.AddAudioSource(_audioSource);

        public override void UnregisterComponent() =>
            Entity.RemoveAudioSource();
    }
}