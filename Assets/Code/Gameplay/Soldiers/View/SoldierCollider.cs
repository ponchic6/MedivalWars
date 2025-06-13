using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Soldiers.View
{
    public class SoldierCollider : MonoBehaviour
    {
        [SerializeField] private EntityBehaviour _entityBehaviour;

        private void OnTriggerEnter(Collider other)
        {
            EntityBehaviour otherEntityBehavior = other.GetComponentInParent<EntityBehaviour>();

            if (otherEntityBehavior == null) 
                return;

            GameEntity otherEntity = otherEntityBehavior.Entity;
            GameEntity thisEntity = _entityBehaviour.Entity;
            
            if (otherEntity.hasSoldierAttackTowerId && 
                otherEntity.towerFraction.Value != thisEntity.towerFraction.Value &&
                otherEntity.soldierTowerOfBirthId.Value == thisEntity.soldierAttackTowerId.Value &&
                otherEntity.soldierAttackTowerId.Value == thisEntity.soldierTowerOfBirthId.Value)
            {
                if (otherEntity.isHorseKnight) 
                    thisEntity.ReplaceSoldierHealth(thisEntity.soldierHealth.Value - 2);
                else
                    thisEntity.ReplaceSoldierHealth(thisEntity.soldierHealth.Value - 1);
                return;
            }
            
            if (otherEntity.id.Value == thisEntity.soldierTowerOfBirthId.Value)
                return;
            
            if (!otherEntity.isTower)
                return;
            
            if (otherEntity.id.Value != thisEntity.soldierAttackTowerId.Value)
                return;

            if (otherEntity.towerFraction.Value == thisEntity.towerFraction.Value)
            {
                thisEntity.ReplaceSoldierHealth(thisEntity.soldierHealth.Value - 2);
                otherEntity.ReplaceLastArrivedKnightType(thisEntity.isHorseKnight ? SoldierType.HorseKnight : SoldierType.Knight);

                if (thisEntity.isHorseKnight) 
                    otherEntity.ReplaceTowerScore(otherEntity.towerScore.Value + 2);
                else
                    otherEntity.ReplaceTowerScore(otherEntity.towerScore.Value + 1);
            }
            else
            {
                thisEntity.ReplaceSoldierHealth(thisEntity.soldierHealth.Value - 2);
                otherEntity.ReplaceLastArrivedKnightType(thisEntity.isHorseKnight ? SoldierType.HorseKnight : SoldierType.Knight);

                if (thisEntity.isHorseKnight) 
                    otherEntity.ReplaceTowerScore(otherEntity.towerScore.Value - 2);
                else
                    otherEntity.ReplaceTowerScore(otherEntity.towerScore.Value - 1);

                if (otherEntity.towerScore.Value <= 0)
                {
                    otherEntity.ReplaceTowerScore(0);
                    otherEntity.ReplaceTowerFraction(thisEntity.towerFraction.Value);
                }
            }
        }
    }
}