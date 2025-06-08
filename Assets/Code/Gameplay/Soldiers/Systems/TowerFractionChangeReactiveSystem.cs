using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Soldiers.Systems
{
    public class TowerFractionChangeReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;

        public TowerFractionChangeReactiveSystem(IContext<GameEntity> context) : base(context)
        {
            _game = Contexts.sharedInstance.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.TowerFraction);

        protected override bool Filter(GameEntity entity) =>
            entity.isTower;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity towerEntity in entities)
            {
                towerEntity.towerFractionColorController.Value.SetFractionColor(towerEntity.towerFraction.Value);
                
                if (towerEntity.isCatapult)
                    continue;
                
                foreach (int routeId in towerEntity.towerRouteIdList.Value)
                    _game.GetEntityWithId(routeId).isDestructed = true;
            }
        }
    }
}