using System.Collections.Generic;
using Code.Gameplay.Towers;
using Code.Infrastructure.Services;
using Code.Infrastructure.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Map.Systems
{
    public class MapInitializeSystem : IInitializeSystem
    {
        private readonly IIdentifierService _identifierService;
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;

        public MapInitializeSystem(IIdentifierService identifierService, CommonStaticData commonStaticData)
        {
            _identifierService = identifierService;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
        }
        
        public void Initialize()
        {
            CreateTower(Vector3.zero + Vector3.left * 0.5f + Vector3.back * 0.5f, TowerFractionsEnum.Blue, 5);
            CreateTower(Vector3.zero + Vector3.right * 0.5f + Vector3.back * 0.5f, TowerFractionsEnum.Blue, 8);
            CreateTower(Vector3.zero + Vector3.left * 0.5f + Vector3.forward * 0.5f, TowerFractionsEnum.Red, 17);
            CreateTower(Vector3.zero + Vector3.right * 0.5f + Vector3.forward * 0.5f, TowerFractionsEnum.Neutral, 2);
        }

        private void CreateTower(Vector3 startPos, TowerFractionsEnum fraction, int initialTowerScore)
        {
            GameEntity towerEntity = _game.CreateEntity();
            towerEntity.AddId(_identifierService.Next());
            towerEntity.AddInitialTransform(startPos, Quaternion.Euler(0, -60, -0));
            towerEntity.AddTowerScore(initialTowerScore);
            towerEntity.AddViewPrefab(_commonStaticData.towerPrefab);
            towerEntity.AddUsedRouteCount(0);
            towerEntity.AddSoldierCreationCooldown(_commonStaticData.soldierCreationCooldownZeroLevel);
            towerEntity.AddTowerScoreIncreasingCooldown(_commonStaticData.towerScoreIncreasingCooldown);
            towerEntity.AddTowerRouteIdList(new List<int>());
            towerEntity.isTower = true;
            towerEntity.AddTowerFraction(fraction);
        }
    }
}