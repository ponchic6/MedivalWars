//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Code.Gameplay.Soldiers.SoldierDeathViewComponent soldierDeathView { get { return (Code.Gameplay.Soldiers.SoldierDeathViewComponent)GetComponent(GameComponentsLookup.SoldierDeathView); } }
    public bool hasSoldierDeathView { get { return HasComponent(GameComponentsLookup.SoldierDeathView); } }

    public void AddSoldierDeathView(Code.Gameplay.Soldiers.View.SoldierDeathView newValue) {
        var index = GameComponentsLookup.SoldierDeathView;
        var component = (Code.Gameplay.Soldiers.SoldierDeathViewComponent)CreateComponent(index, typeof(Code.Gameplay.Soldiers.SoldierDeathViewComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceSoldierDeathView(Code.Gameplay.Soldiers.View.SoldierDeathView newValue) {
        var index = GameComponentsLookup.SoldierDeathView;
        var component = (Code.Gameplay.Soldiers.SoldierDeathViewComponent)CreateComponent(index, typeof(Code.Gameplay.Soldiers.SoldierDeathViewComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveSoldierDeathView() {
        RemoveComponent(GameComponentsLookup.SoldierDeathView);
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

    static Entitas.IMatcher<GameEntity> _matcherSoldierDeathView;

    public static Entitas.IMatcher<GameEntity> SoldierDeathView {
        get {
            if (_matcherSoldierDeathView == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SoldierDeathView);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSoldierDeathView = matcher;
            }

            return _matcherSoldierDeathView;
        }
    }
}
