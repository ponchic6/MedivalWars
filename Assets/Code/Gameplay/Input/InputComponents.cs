using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.Input
{
    [Game, Unique] public class Input : IComponent { }
    [Game, Unique] public class TapHold : IComponent { }
    [Game, Unique] public class PlayClick : IComponent { }
    [Game, Unique] public class NextLevelClick : IComponent { }
    [Game, Unique] public class PreviousLevelClick : IComponent { }
    [Game, Unique] public class MapRestartClick : IComponent { }
    [Game, Unique] public class ChoseLevel : IComponent { public int Value;}
}