using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Gameplay.Audio.View
{
    public class AudioSourceController : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> _audioSources;
        private GameContext _game;

        public void Awake()
        {
            _game = Contexts.sharedInstance.game;
        }

        public void Play(AudioClip clip, bool loop = false)
        {
            AudioSource audioSource = _audioSources.Find(x => !x.isPlaying);
            
            if (audioSource == null)
                return;

            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }

        public void SetVolume(float volume) =>
            _audioSources.ForEach(x => x.volume = volume);
    }
}