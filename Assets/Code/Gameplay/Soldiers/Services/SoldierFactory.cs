using Code.Gameplay.Towers;
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
        private readonly Random _random;

        public SoldierFactory(IIdentifierService identifierService, CommonStaticData commonStaticData)
        {
            _identifierService = identifierService;
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
            _random = new Random();
        }
        
        public void CreateSoldier(GameEntity startTower)
        {
            int routeId = startTower.towerRouteIdList.Value[_random.Next(startTower.towerRouteIdList.Value.Count)];
            GameEntity route = _game.GetEntityWithId(routeId);
            GameEntity soldier = _game.CreateEntity();
            soldier.AddId(_identifierService.Next());
            soldier.AddTowerFraction(startTower.towerFraction.Value);
            switch (startTower.towerFraction.Value)
            {
                case TowerFractionsEnum.Red:
                    soldier.AddViewPrefab(_commonStaticData.redSoldierPrefab);
                    break;
                case TowerFractionsEnum.Blue:
                    soldier.AddViewPrefab(_commonStaticData.blueSoldierPrefab);
                    break;
            }
            soldier.AddSoldierTowerOfBirthId(startTower.id.Value);
            soldier.AddInitialTransform(startTower.transform.Value.position + _commonStaticData.verticalRouteOffset * Vector3.up, Quaternion.identity);
            soldier.AddSoldierAttackTowerId(route.routeFinishId.Value);
        }
    }
}