//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Code.Gameplay.Towers.TowerUiViewComponent towerUiView { get { return (Code.Gameplay.Towers.TowerUiViewComponent)GetComponent(GameComponentsLookup.TowerUiView); } }
    public bool hasTowerUiView { get { return HasComponent(GameComponentsLookup.TowerUiView); } }

    public void AddTowerUiView(Code.Gameplay.Towers.View.TowerUiView newValue) {
        var index = GameComponentsLookup.TowerUiView;
        var component = (Code.Gameplay.Towers.TowerUiViewComponent)CreateComponent(index, typeof(Code.Gameplay.Towers.TowerUiViewComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTowerUiView(Code.Gameplay.Towers.View.TowerUiView newValue) {
        var index = GameComponentsLookup.TowerUiView;
        var component = (Code.Gameplay.Towers.TowerUiViewComponent)CreateComponent(index, typeof(Code.Gameplay.Towers.TowerUiViewComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTowerUiView() {
        RemoveComponent(GameComponentsLookup.TowerUiView);
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

    static Entitas.IMatcher<GameEntity> _matcherTowerUiView;

    public static Entitas.IMatcher<GameEntity> TowerUiView {
        get {
            if (_matcherTowerUiView == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TowerUiView);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTowerUiView = matcher;
            }

            return _matcherTowerUiView;
        }
    }
}
