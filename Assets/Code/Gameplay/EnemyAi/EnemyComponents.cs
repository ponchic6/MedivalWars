using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.EnemyAi
{
    [Game, Unique] public class EnemyActionCooldown : IComponent { public float Value; }
    [Game, Unique] public class EnemyReadyToAction : IComponent { }
}