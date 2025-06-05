using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "CommonStaticData", menuName = "StaticData/CommonStaticData")]
    public class CommonStaticData : ScriptableObject
    {
        public int scoreFromZeroToFirstLevel;
        public int scoreFromFirstToSecondLevel;
        public EntityBehaviour towerPrefab;
        public EntityBehaviour routePrefab;
        public EntityBehaviour blueSoldierPrefab;
        public EntityBehaviour redSoldierPrefab;
        public float soldierCreationCooldownZeroLevel;
        public float soldierCreationCooldownFirstLevel;
        public float soldierCreationCooldownSecondLevel;
        public float soldierMoveSpeed;
        public float towerScoreIncreasingCooldown;
        public float verticalRouteOffset;
    }
}