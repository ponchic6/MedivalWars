using Code.Infrastructure.Services;
using Code.Infrastructure.StaticData;
using UnityEngine;

namespace Code.Gameplay.Towers.Services
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly CommonStaticData _commonStaticData;
        private readonly IIdentifierService _identifierService;
        private readonly GameContext _game;

        public ProjectileFactory(CommonStaticData commonStaticData, IIdentifierService identifierService)
        {
            _commonStaticData = commonStaticData;
            _identifierService = identifierService;
            _game = Contexts.sharedInstance.game;
        }
        
        public void CreateProjectile(GameEntity catapult, GameEntity target)
        {
            GameEntity projectile = _game.CreateEntity();
            projectile.AddId(_identifierService.Next());
            projectile.isProjectile = true;
            projectile.AddViewPrefab(_commonStaticData.projectilePrefab);
            projectile.AddInitialTransform(catapult.transform.Value.position, Quaternion.identity);
            projectile.AddProjectileTargetId(target.id.Value);
        }
    }
}