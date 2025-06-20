﻿using Code.Gameplay.Towers;
using UnityEngine;

namespace Code.Gameplay.Levels
{
    public class LevelObjectMarker : MonoBehaviour
    {
        public TowerFractionsEnum towerFractionsEnum;
        public TowerTypesEnum towerType;
        [Range(0, 65)] public int initialTowerScore;
    }
}