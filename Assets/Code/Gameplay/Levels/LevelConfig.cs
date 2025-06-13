using System.Collections.Generic;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelSystem/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public List<LevelObjectData> levelObjects = new();
        public EntityBehaviour obstaclePrefab;
    }
}

