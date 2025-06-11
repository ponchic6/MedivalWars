using Code.Gameplay.UI.View;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.UI
{
    [Game, Unique] public class HudCanvas : IComponent { }
    [Game] public class HudHandlerComponent : IComponent { public HudHandler Value; }
}