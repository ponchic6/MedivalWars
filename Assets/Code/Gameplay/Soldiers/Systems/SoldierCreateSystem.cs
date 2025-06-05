using Code.Gameplay.Soldiers.Services;
using Code.Infrastructure.StaticData;
using Entitas;

namespace Code.Gameplay.Soldiers.Systems
{
    public class SoldierCreateSystem : IExecuteSystem
    {
        private readonly ISoldierFactory _soldierFactory;
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public SoldierCreateSystem(ISoldierFactory soldierFactory, CommonStaticData commonStaticData)
        {
            _soldierFactory = soldierFactory;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.ReadyToCreateSoldier);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities())
            {
                if (entity.towerRouteIdList.Value.Count == 0)
                    continue;
                
                entity.isReadyToCreateSoldier = false;
                
                switch (entity.towerLevel.Value)
                {
                    case 0:
                        entity.ReplaceSoldierCreationCooldown(_commonStaticData.soldierCreationCooldownZeroLevel);
                        break; 
                    case 1:
                        entity.ReplaceSoldierCreationCooldown(_commonStaticData.soldierCreationCooldownFirstLevel);
                        break;
                    case 2:
                        entity.ReplaceSoldierCreationCooldown(_commonStaticData.soldierCreationCooldownSecondLevel);
                        break;
                }
                
                _soldierFactory.CreateSoldier(entity);
            }
        }
    }
}