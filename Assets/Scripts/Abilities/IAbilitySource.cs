using Characters;

namespace Abilities
{
    public interface IAbilitySource
    {
        public string GetName();
        public bool EligibleForAbility(Ability ability, ICharacter character);
    }
}