using Characters;
using Combat;

namespace Abilities
{
    [System.Serializable]
    public class AbilityRequirementStrategy
    {
        public string Name;

        public virtual bool Evaluate(BattleRound e, CharacterBattleAlias user, CharacterBattleAlias target)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class UserHasAPStrategy : AbilityRequirementStrategy
    {
        public int APRequired;
        public override bool Evaluate(BattleRound e, CharacterBattleAlias user, CharacterBattleAlias target)
        {
            return user.AP >= APRequired;
        }
    }
    
    public class UserHasPPStrategy : AbilityRequirementStrategy
    {
        public int PPRequired;
        public override bool Evaluate(BattleRound e, CharacterBattleAlias user, CharacterBattleAlias target)
        {
            return user.PP >= PPRequired;
        }
    }
    
    
}