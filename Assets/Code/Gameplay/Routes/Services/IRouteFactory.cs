namespace Code.Gameplay.Routes.Services
{
    public interface IRouteFactory
    {
        public void CreateDraggingRoute(GameEntity start);
        public void CreateRoute(GameEntity start, GameEntity end);
    }
}