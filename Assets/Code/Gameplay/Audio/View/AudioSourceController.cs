using System;
using UnityEngine;

namespace Code.Gameplay.Audio.View
{
    public class AudioSourceController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        private GameContext _game;

        public void Awake()
        {
            _game = Contexts.sharedInstance.game;
        }

        public void Play(AudioClip clip)
        {
            if (!_game.mainAudioEntity.isAudioOn)
                return;
            
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}