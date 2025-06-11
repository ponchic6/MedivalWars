using Code.Gameplay.Towers;
using UnityEngine;

namespace Code.Gameplay.Levels
{
    [System.Serializable]
    public class LevelObjectData
    {
        public Vector3 position;
        public TowerFractionsEnum towerFraction;
        public TowerTypesEnum towerType;
        public int initialTowerScore;
    }
}