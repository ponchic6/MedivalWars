﻿using Code.Gameplay.Towers;
using Code.Infrastructure.Services;
using Code.Infrastructure.StaticData;
using UnityEngine;
using Random = System.Random;

namespace Code.Gameplay.Soldiers.Services
{
    public class SoldierFactory : ISoldierFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;

        public SoldierFactory(IIdentifierService identifierService, CommonStaticData commonStaticData)
        {
            _identifierService = identifierService;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
        }
        
        public void CreateSoldier(GameEntity startTower, SoldierType value, int routeId)
        {
            GameEntity route = _game.GetEntityWithId(routeId);
            GameEntity soldier = _game.CreateEntity();
            soldier.AddId(_identifierService.Next());
            soldier.AddTowerFraction(startTower.towerFraction.Value);
            
            if (startTower.towerFraction.Value == TowerFractionsEnum.Red && value == SoldierType.Knight)
                soldier.AddViewPrefab(_commonStaticData.redSoldierPrefab);
            else if (startTower.towerFraction.Value == TowerFractionsEnum.Red && value == SoldierType.HorseKnight)
                soldier.AddViewPrefab(_commonStaticData.redHorseKnightPrefab);
            else if (startTower.towerFraction.Value == TowerFractionsEnum.Blue && value == SoldierType.Knight)
                soldier.AddViewPrefab(_commonStaticData.blueSoldierPrefab);
            else if (startTower.towerFraction.Value == TowerFractionsEnum.Blue && value == SoldierType.HorseKnight)
                soldier.AddViewPrefab(_commonStaticData.blueHorseKnightPrefab);

            soldier.AddSoldierTowerOfBirthId(startTower.id.Value);
            soldier.AddInitialTransform(startTower.transform.Value.position + _commonStaticData.verticalRouteOffset * Vector3.up, Quaternion.identity);
            soldier.AddSoldierAttackTowerId(route.routeFinishId.Value);
            soldier.AddSoldierHealth(value == SoldierType.HorseKnight ? 2 : 1);
            soldier.isHorseKnight = value == SoldierType.HorseKnight;
        }
    }
}