namespace Code.Gameplay.Towers.Services
{
    public interface IProjectileFactory
    {
        public void CreateProjectile(GameEntity catapult, GameEntity target);
    }
}