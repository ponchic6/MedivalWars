using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Soldiers.View
{
    public class SoldierCollider : MonoBehaviour
    {
        [SerializeField] private EntityBehaviour _entityBehaviour;
        private GameContext _game;

        private void Awake()
        {
            _game = Contexts.sharedInstance.game;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent<EntityBehaviour>(out var otherEntityBehavior);
            
            if (otherEntityBehavior == null) 
                return;

            if (otherEntityBehavior.Entity.hasSoldierAttackTowerId && 
                otherEntityBehavior.Entity.towerFraction.Value != _entityBehaviour.Entity.towerFraction.Value)
            {
                otherEntityBehavior.Entity.isDestructed = true;
                _entityBehaviour.Entity.isDestructed = true;
                return;
            }
            
            if (otherEntityBehavior.Entity.id.Value == _entityBehaviour.Entity.soldierTowerOfBirthId.Value)
                return;
            
            if (!otherEntityBehavior.Entity.isTower)
                return;
            
            if (otherEntityBehavior.Entity.towerFraction.Value != _entityBehaviour.Entity.towerFraction.Value)
            {
                _entityBehaviour.Entity.isDestructed = true;
                
                if (otherEntityBehavior.Entity.towerScore.Value > 0)
                {
                    otherEntityBehavior.Entity.towerScore.Value--;
                    otherEntityBehavior.Entity.ReplaceTowerScore(otherEntityBehavior.Entity.towerScore.Value);
                }
                else
                    otherEntityBehavior.Entity.ReplaceTowerFraction(_entityBehaviour.Entity.towerFraction.Value);
            }
            
            if (otherEntityBehavior.Entity.towerFraction.Value == _entityBehaviour.Entity.towerFraction.Value)
            {
                _entityBehaviour.Entity.isDestructed = true;
                otherEntityBehavior.Entity.towerScore.Value++;
                otherEntityBehavior.Entity.ReplaceTowerScore(otherEntityBehavior.Entity.towerScore.Value);
            }
        }
    }
}