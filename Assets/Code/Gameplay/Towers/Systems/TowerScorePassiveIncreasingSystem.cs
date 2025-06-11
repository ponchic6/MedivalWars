using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Towers.Systems
{
    public class TowerScorePassiveIncreasingSystem : IExecuteSystem
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public TowerScorePassiveIncreasingSystem(CommonStaticData commonStaticData)
        {
            _game = Contexts.sharedInstance.game;
            _commonStaticData = commonStaticData;

            _entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.TowerScore, GameMatcher.TowerScoreIncreasingCooldown).NoneOf(GameMatcher.Catapult));
        }
        
        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.towerFraction.Value == TowerFractionsEnum.Neutral)
                    continue;
                
                if (entity.towerScoreIncreasingCooldown.Value <= 0)
                {
                    if (entity.towerRouteIdList.Value.Count != 0)
                        continue;
                    
                    if (entity.towerScore.Value >= _commonStaticData.maxScore)
                        continue;

                    RefreshIncreasingCooldown(entity);
                    entity.ReplaceTowerScore(entity.towerScore.Value + 1);
                }
                
                entity.towerScoreIncreasingCooldown.Value -= Time.deltaTime;
            }
        }

        private void RefreshIncreasingCooldown(GameEntity entity)
        {
            switch (entity.towerLevel.Value)
            {
                case 0:
                    entity.ReplaceTowerScoreIncreasingCooldown(_commonStaticData.towerScoreIncreasingCooldownZeroLevel);
                    break; 
                case 1:
                    entity.ReplaceTowerScoreIncreasingCooldown(_commonStaticData.towerScoreIncreasingCooldownFirstLevel);
                    break;
                case 2:
                    entity.ReplaceTowerScoreIncreasingCooldown(_commonStaticData.towerScoreIncreasingCooldownSecondLevel);
                    break;
            }
        }
    }
}