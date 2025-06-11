using System.Collections.Generic;
using Code.Gameplay.Levels;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "CommonStaticData", menuName = "StaticData/CommonStaticData")]
    public class CommonStaticData : ScriptableObject
    {
        public List<LevelConfig> levels;
        public int scoreFromZeroToFirstLevel;
        public int scoreFromFirstToSecondLevel;
        public EntityBehaviour towerPrefab;
        public EntityBehaviour hippodromePrefab;
        public EntityBehaviour catapultPrefab;
        public EntityBehaviour projectilePrefab;
        public EntityBehaviour routePrefab;
        public EntityBehaviour blueSoldierPrefab;
        public EntityBehaviour redSoldierPrefab;
        public EntityBehaviour blueHorseKnightPrefab;
        public EntityBehaviour redHorseKnightPrefab;
        public float soldierCreationCooldownZeroLevel;
        public float soldierCreationCooldownFirstLevel;
        public float soldierCreationCooldownSecondLevel;
        public float soldierMoveSpeed;
        public float verticalRouteOffset;
        public float enemyActionCooldown;
        public float catapultCooldownZeroLevel;
        public float catapultCooldownFirstLevel;
        public float catapultCooldownSecondLevel;
        public float catapultRangeZeroLevel;
        public float catapultRangeFirstLevel;
        public float catapultRangeSecondLevel;
        public float projectileSpeed;
        public int maxScore;
        public float towerScoreIncreasingCooldownZeroLevel;
        public float towerScoreIncreasingCooldownFirstLevel;
        public float towerScoreIncreasingCooldownSecondLevel;
        public EntityBehaviour canvasPrefab;
    }
}