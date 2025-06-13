namespace Code.Gameplay.Soldiers.Services
{
    public interface ISoldierFactory
    {
        public void CreateSoldier(GameEntity startTower, SoldierType type, int routeId);
    }
}