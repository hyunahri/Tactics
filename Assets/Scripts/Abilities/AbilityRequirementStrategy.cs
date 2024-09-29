using Characters;
using Combat;

namespace Abilities
{
    public abstract class AbilityRequirementStrategy
    {
        public string Name;
        public abstract bool Evaluate(BattleRound e, CharacterBattleAlias user, CharacterBattleAlias target);
    }
}