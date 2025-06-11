using System.Collections.Generic;
using Code.Gameplay.Towers.View;
using Entitas;

namespace Code.Gameplay.Towers
{
    [Game] public class TowerScore : IComponent { public int Value; }
    [Game] public class TowerUiViewComponent : IComponent { public TowerUiView Value; }
    [Game] public class MaxRouteCount : IComponent { public int Value; }
    [Game] public class UsedRouteCount : IComponent { public int Value; }
    [Game] public class TowerLevel : IComponent { public int Value; }
    [Game] public class TowerRouteIdList : IComponent { public List<int> Value; }
    [Game] public class Tower : IComponent { }
    [Game] public class Catapult : IComponent { }
    [Game] public class Hippodrome : IComponent { }
    [Game] public class Obstacle : IComponent { }
    [Game] public class Projectile : IComponent { }
    [Game] public class ProjectileTargetId : IComponent { public int Value; }
    [Game] public class CatapultShootingCooldownComponent : IComponent { public float Value; }
    [Game] public class TowerFraction : IComponent { public TowerFractionsEnum Value; }
    [Game] public class TowerFractionColorControllerComponent : IComponent { public TowerColorController Value; }
    [Game] public class TowerLevelViewControllerComponent : IComponent { public TowerLevelViewController Value; }
    [Game] public class SoldierCreationCooldown : IComponent { public float Value; }
    [Game] public class TowerScoreIncreasingCooldown : IComponent { public float Value; }
}