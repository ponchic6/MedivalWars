using Code.Gameplay.Soldiers.View;
using Entitas;

namespace Code.Gameplay.Soldiers
{
    [Game] public class SoldierAttackTowerId : IComponent { public int Value; }
    [Game] public class SoldierTowerOfBirthId : IComponent { public int Value; }
    [Game] public class SoldierHealth : IComponent { public int Value; }
    [Game] public class SoldierDeathViewComponent : IComponent { public SoldierDeathView Value; }
    [Game] public class HorseKnight : IComponent { }
}