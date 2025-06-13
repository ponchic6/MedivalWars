using Entitas;

namespace Code.Gameplay.Input.Systems
{
    public class HoldMouseButtonInputSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public HoldMouseButtonInputSystem()
        {
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.Input);
        }
        
        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                entity.isTapHold = UnityEngine.Input.GetMouseButton(0);
            }
        }
    }
}