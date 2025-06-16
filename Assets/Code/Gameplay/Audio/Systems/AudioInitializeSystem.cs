using Code.Infrastructure.Services;
using Code.Infrastructure.StaticData;
using Entitas;

namespace Code.Gameplay.Audio.Systems
{
    public class AudioInitializeSystem : IInitializeSystem
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly IIdentifierService _identifierService;
        private readonly GameContext _game;

        public AudioInitializeSystem(CommonStaticData commonStaticData, IIdentifierService identifierService)
        {
            _identifierService = identifierService;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
        }
        
        public void Initialize()
        {
            GameEntity audioEntity = _game.CreateEntity();
            audioEntity.AddId(_identifierService.Next());
            audioEntity.isMainAudio = true;
            audioEntity.AddViewPrefab(_commonStaticData.mainAudioPrefab);
            audioEntity.AddSoldierDeathAudioCooldown(0f);
        }
    }
}