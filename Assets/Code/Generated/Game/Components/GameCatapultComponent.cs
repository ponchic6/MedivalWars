//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Code.Gameplay.Towers.Catapult catapultComponent = new Code.Gameplay.Towers.Catapult();

    public bool isCatapult {
        get { return HasComponent(GameComponentsLookup.Catapult); }
        set {
            if (value != isCatapult) {
                var index = GameComponentsLookup.Catapult;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : catapultComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCatapult;

    public static Entitas.IMatcher<GameEntity> Catapult {
        get {
            if (_matcherCatapult == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Catapult);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCatapult = matcher;
            }

            return _matcherCatapult;
        }
    }
}
