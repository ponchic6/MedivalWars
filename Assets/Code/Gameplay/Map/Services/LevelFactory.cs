using System.Collections.Generic;
using Code.Gameplay.Levels;
using Code.Gameplay.Towers;
using Code.Infrastructure.Services;
using Code.Infrastructure.StaticData;
using Code.Infrastructure.View;
using Cysharp.Threading.Tasks;
using Entitas;
using UnityEngine;
using Random = System.Random;

namespace Code.Gameplay.Map.Services
{
    public class LevelFactory : ILevelFactory
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly GameContext _game;
        private readonly IIdentifierService _identifierService;

        public LevelFactory(CommonStaticData commonStaticData, IIdentifierService identifierService)
        {
            _commonStaticData = commonStaticData;
            _game = Contexts.sharedInstance.game;
            _identifierService = identifierService;
        }

        public async void SetDecorLevel(int levelIndex)
        {
            await UniTask.DelayFrame(2);
            
            foreach (LevelObjectData levelObjectData in _commonStaticData.levels[levelIndex].levelObjects) 
                CreateDecorTower(levelObjectData.position, levelObjectData.towerFraction, levelObjectData.initialTowerScore, levelObjectData.towerType);

            EntityBehaviour obstaclePrefab = _commonStaticData.levels[levelIndex].obstaclePrefab;

            if (obstaclePrefab == null)
                return;
            
            GameEntity obstacleEntity = _game.CreateEntity();
            obstacleEntity.AddId(_identifierService.Next());
            obstacleEntity.isObstacle = true;
            obstacleEntity.AddViewPrefab(obstaclePrefab);
        }

        public async void StartLevel(int levelIndex)
        {
            await UniTask.DelayFrame(2);
            
            foreach (LevelObjectData levelObjectData in _commonStaticData.levels[levelIndex].levelObjects) 
                CreateTower(levelObjectData.position, levelObjectData.towerFraction, levelObjectData.initialTowerScore, levelObjectData.towerType);

            EntityBehaviour obstaclePrefab = _commonStaticData.levels[levelIndex].obstaclePrefab;

            if (obstaclePrefab == null)
                return;
            
            GameEntity obstacleEntity = _game.CreateEntity();
            obstacleEntity.AddId(_identifierService.Next());
            obstacleEntity.isObstacle = true;
            obstacleEntity.AddViewPrefab(obstaclePrefab);
        }

        public void RestartLevel() => 
            StartLevel(_game.choseLevel.Value);
        
        public void CleanMap()
        {
            IGroup<GameEntity> routes = _game.GetGroup(GameMatcher.Route);
            IGroup<GameEntity> towers = _game.GetGroup(GameMatcher.Tower);
            IGroup<GameEntity> soldiers = _game.GetGroup(GameMatcher.SoldierHealth);
            IGroup<GameEntity> projectiles = _game.GetGroup(GameMatcher.Projectile);
            IGroup<GameEntity> obstacles = _game.GetGroup(GameMatcher.Obstacle);

            foreach (GameEntity route in routes)
                route.isDestructed = true;
            
            foreach (GameEntity tower in towers)
                tower.isDestructed = true;

            foreach (GameEntity soldier in soldiers)
                soldier.isDestructed = true;

            foreach (GameEntity projectile in projectiles)
                projectile.isDestructed = true;
            
            foreach (GameEntity obstacle in obstacles)
                obstacle.isDestructed = true;
        }

        private void CreateDecorTower(Vector3 startPos, TowerFractionsEnum fraction, int initialTowerScore, TowerTypesEnum towerType)
        {
            GameEntity towerEntity = _game.CreateEntity();
            towerEntity.AddId(_identifierService.Next());
            towerEntity.AddInitialTransform(startPos, Quaternion.Euler(0, -60, -0));
            towerEntity.AddTowerScore(initialTowerScore);
            towerEntity.AddTowerFraction(fraction);
            towerEntity.isTower = true;
            towerEntity.isDecor = true;
            
            switch (towerType)
            {
                case TowerTypesEnum.Tower:
                    towerEntity.AddViewPrefab(_commonStaticData.towerPrefab);
                    break;
                case TowerTypesEnum.Hippodrome:
                    towerEntity.AddViewPrefab(_commonStaticData.hippodromePrefab);
                    break;
                case TowerTypesEnum.Catapult:
                    towerEntity.AddViewPrefab(_commonStaticData.catapultPrefab);
                    break;
            }
        }

        private void CreateTower(Vector3 startPos, TowerFractionsEnum fraction, int initialTowerScore, TowerTypesEnum towerType)
        {
            GameEntity towerEntity = _game.CreateEntity();
            towerEntity.AddId(_identifierService.Next());
            towerEntity.AddInitialTransform(startPos, Quaternion.Euler(0, -60, -0));
            towerEntity.AddTowerScore(initialTowerScore);
            towerEntity.AddTowerFraction(fraction);
            towerEntity.isTower = true;
            
            switch (towerType)
            {
                case TowerTypesEnum.Tower:
                    towerEntity.AddViewPrefab(_commonStaticData.towerPrefab);
                    towerEntity.AddUsedRouteCount(0);
                    towerEntity.AddSoldierCreationCooldown(_commonStaticData.soldierCreationCooldownZeroLevel);
                    towerEntity.AddTowerRouteIdList(new List<int>());
                    break;
                case TowerTypesEnum.Hippodrome:
                    towerEntity.AddViewPrefab(_commonStaticData.hippodromePrefab);
                    towerEntity.AddUsedRouteCount(0);
                    towerEntity.AddSoldierCreationCooldown(_commonStaticData.soldierCreationCooldownZeroLevel);
                    towerEntity.AddTowerRouteIdList(new List<int>());
                    towerEntity.isHippodrome = true;
                    break;
                case TowerTypesEnum.Catapult:
                    towerEntity.AddViewPrefab(_commonStaticData.catapultPrefab);
                    towerEntity.isCatapult = true;
                    towerEntity.AddCatapultShootingCooldown(_commonStaticData.catapultCooldownZeroLevel);
                    break;
            }
        }
    }
}