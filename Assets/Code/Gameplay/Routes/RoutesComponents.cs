using Code.Gameplay.Routes.View;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.Routes
{
    [Game] public class RouteStartId : IComponent { public int Value; }
    [Game] public class RouteFinishId : IComponent { public int Value; }
    [Game] public class RoutesColorControllerComponent : IComponent { public RoutesColorController Value; }
    [Game] public class Route : IComponent { }
    [Game] public class RouteReady : IComponent { }
    [Game, Unique] public class RouteIntersectingObstacle : IComponent { }
    [Game] public class RouteDistance : IComponent { public float Value;}
    [Game, Unique] public class StartTowerRoutesPoint : IComponent { }
    [Game, Unique] public class FinishTowerRoutesPoint : IComponent { }
}