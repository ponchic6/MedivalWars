using Code.Gameplay.Map.Services;
using Entitas;

namespace Code.Gameplay.Map.Systems
{
    public class MapInitializeSystem : IInitializeSystem
    {
        private readonly ILevelFactory _levelFactory;
        private readonly GameContext _game;

        public MapInitializeSystem(ILevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
            _game = Contexts.sharedInstance.game;
        }

        public void Initialize()
        {
            _game.inputEntity.ReplaceChoseLevel(0);
            _levelFactory.SetDecorLevel(0);
        }
    }
}