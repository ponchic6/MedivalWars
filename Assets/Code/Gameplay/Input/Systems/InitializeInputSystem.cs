using Entitas;

namespace Code.Gameplay.Input.Systems
{
    public class InitializeInputSystem : IInitializeSystem
    {
        private GameContext _game;

        public void Initialize()
        {
            _game = Contexts.sharedInstance.game;

            GameEntity inputEntity = _game.CreateEntity();
            inputEntity.isInput = true;
        }
    }
}