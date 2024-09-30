using Characters;

namespace Abilities
{
    /// <summary>
    /// Container for abilities which provides information on the source of the ability.
    /// We can use this to ensure abilities are added and removed to match the character's current state.
    /// </summary>
    public class OwnedAbility
    {
        public OwnedAbility(Ability ability, IAbilitySource source)
        {
            Ability = ability;
            Source = source;
        }
        
        public Ability Ability;
        public IAbilitySource Source;
        
        public bool IsEligible(ICharacter character) => Source.EligibleForAbility(Ability, character);
    }
}