using Code.Gameplay.Audio.View;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.Audio
{
    [Game, Unique] public class MainAudio : IComponent { }
    [Game] public class AudioSourceComponent : IComponent { public AudioSourceController Value; }
    [Game] public class AudioOn : IComponent { }
    [Game] public class SoldierDeathAudioCooldown : IComponent { public float Value; }
}