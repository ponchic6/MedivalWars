using Code.Gameplay.Soldiers.Services;
using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Towers.Systems
{
    public class SoldierCreationSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;
        private readonly ISoldierFactory _soldierFactory;
        private readonly CommonStaticData _commonStaticData;

        public SoldierCreationSystem(ISoldierFactory soldierFactory, CommonStaticData commonStaticData)
        {
            _soldierFactory = soldierFactory;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;

            _entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.SoldierCreationCooldown, GameMatcher.Tower));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.soldierCreationCooldown.Value <= 0)
                {
                    if (entity.towerRouteIdList.Value.Count == 0)
                        continue;
                    
                    RefreshCreationCooldown(entity);
                    foreach (int routeId in entity.towerRouteIdList.Value) 
                        _soldierFactory.CreateSoldier(entity, routeId);
                }
                
                entity.soldierCreationCooldown.Value -= Time.deltaTime;
            }
        }

        private void RefreshCreationCooldown(GameEntity entity)
        {
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
        }
    }
}