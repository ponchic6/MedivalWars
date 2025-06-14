//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Code.Gameplay.Soldiers.SoldierAttackTowerId soldierAttackTowerId { get { return (Code.Gameplay.Soldiers.SoldierAttackTowerId)GetComponent(GameComponentsLookup.SoldierAttackTowerId); } }
    public bool hasSoldierAttackTowerId { get { return HasComponent(GameComponentsLookup.SoldierAttackTowerId); } }

    public void AddSoldierAttackTowerId(int newValue) {
        var index = GameComponentsLookup.SoldierAttackTowerId;
        var component = (Code.Gameplay.Soldiers.SoldierAttackTowerId)CreateComponent(index, typeof(Code.Gameplay.Soldiers.SoldierAttackTowerId));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceSoldierAttackTowerId(int newValue) {
        var index = GameComponentsLookup.SoldierAttackTowerId;
        var component = (Code.Gameplay.Soldiers.SoldierAttackTowerId)CreateComponent(index, typeof(Code.Gameplay.Soldiers.SoldierAttackTowerId));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveSoldierAttackTowerId() {
        RemoveComponent(GameComponentsLookup.SoldierAttackTowerId);
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

    static Entitas.IMatcher<GameEntity> _matcherSoldierAttackTowerId;

    public static Entitas.IMatcher<GameEntity> SoldierAttackTowerId {
        get {
            if (_matcherSoldierAttackTowerId == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SoldierAttackTowerId);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSoldierAttackTowerId = matcher;
            }

            return _matcherSoldierAttackTowerId;
        }
    }
}
