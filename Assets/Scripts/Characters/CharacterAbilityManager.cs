using System.Collections.Generic;
using System.Linq;
using Abilities;

namespace Characters
{
    /// <summary>
    /// Manages the abilities of a character, including which abilities they have, which are slotted, and the order of those abilities.
    /// </summary>
    public class CharacterAbilityManager
    {
        public CharacterAbilityManager(Character character) => this.character = character;
        private Character character;
        
        //Owned Abilities
        public List<OwnedAbility> OwnedAbilities = new ();
        public void CleanupAbilities()
        {
            OwnedAbilities = OwnedAbilities.Where(a => a.IsEligible(character)).ToList();
            SlottedAbilities.RemoveAll(x => x.Owned == null || !OwnedAbilities.Contains(x.Owned));
        }


        //Slotted Abilities
        public List<SlottedAbility> SlottedAbilities = new ();
        public void SortAbilities() => SlottedAbilities = SlottedAbilities.OrderBy(a => a.ability.ActionType).ThenBy(a => a.Priority).ToList();
        public List<SlottedAbility> GetOrderedAbilitiesByType(ActionTypes type_) => SlottedAbilities.Where(a => a.ability.ActionType == type_).OrderBy(a => a.Priority).ToList();

        
        
        
    }
}