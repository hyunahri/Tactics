using System;
using System.Collections.Generic;
using Characters;
using Combat;
using Units;

namespace Abilities
{
    [System.Serializable]
    public abstract class AbilityPrioritizationStrategy
    {
        public abstract string Name { get; }
        public virtual string Description { get; } = "";
        public abstract IComparer<CharacterBattleAlias> GetComparer(BattleRound e, CharacterBattleAlias user);
    }
    
    public class HighestHealthPriority : AbilityPrioritizationStrategy
    {
        public override string Name => "Highest Health";
        public override IComparer<CharacterBattleAlias> GetComparer(BattleRound e, CharacterBattleAlias user) => Comparer<CharacterBattleAlias>.Create((a, b) => b.HP.CompareTo(a.HP));
    }
    
    public class LowestHealthPriority : AbilityPrioritizationStrategy
    {
        public override string Name => "Lowest Health";
        public override IComparer<CharacterBattleAlias> GetComparer(BattleRound e, CharacterBattleAlias user) => Comparer<CharacterBattleAlias>.Create((a, b) => a.HP.CompareTo(b.HP));
    }
    
    
}