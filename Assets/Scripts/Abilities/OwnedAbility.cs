using Characters;

namespace Abilities
{
    /// <summary>
    /// Container for abilities which provides information on the source of the ability.
    /// We can use this to ensure abilities are added and removed to match the character's current state.
    /// OwnedAbility is not used directly and a character may have many OwnedAbilities which duplicate the same Ability, ie if a character has two swords with the same ability.
    /// OwnedAbility must be wrapped in a SlottedAbility to be used in combat.
    /// </summary>
    public class OwnedAbility
    {
        public OwnedAbility(Ability ability, IAbilitySource? source)
        {
            Ability = ability;
            Source = source;
        }
        
        public Ability Ability;
        public IAbilitySource? Source; //The source which donated this ability to the character. Usually a class or equipment.
        
        public bool IsEligible(ICharacter character) => Source == null || Source.EligibleForAbility(Ability, character);
    }
}