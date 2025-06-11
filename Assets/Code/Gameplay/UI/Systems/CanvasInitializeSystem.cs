using Code.Infrastructure.Services;
using Code.Infrastructure.StaticData;
using Entitas;

namespace Code.Gameplay.UI.Systems
{
    public class CanvasInitializeSystem : IInitializeSystem
    {
        private readonly IIdentifierService _identifierService;
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;

        public CanvasInitializeSystem(IIdentifierService identifierService, CommonStaticData commonStaticData)
        {
            _identifierService = identifierService;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
        }
        
        public void Initialize()
        {
            GameEntity canvas = _game.CreateEntity();
            canvas.AddId(_identifierService.Next());
            canvas.isHudCanvas = true;
            canvas.AddViewPrefab(_commonStaticData.canvasPrefab);
        }
    }
}