using Code.Infrastructure.StaticData;
using Entitas;

namespace Code.Gameplay.Towers.Systems
{
    public class TowerLevelInitializeSystem : IInitializeSystem
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public TowerLevelInitializeSystem(CommonStaticData commonStaticData)
        {
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.TowerScore);
        }
        
        public void Initialize()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.towerScore.Value <= _commonStaticData.scoreFromZeroToFirstLevel)
                    entity.AddTowerLevel(0);
                if (entity.towerScore.Value > _commonStaticData.scoreFromZeroToFirstLevel)
                    entity.AddTowerLevel(1);
                if (entity.towerScore.Value > _commonStaticData.scoreFromFirstToSecondLevel)
                    entity.AddTowerLevel(2);
            }
        }
    }
}