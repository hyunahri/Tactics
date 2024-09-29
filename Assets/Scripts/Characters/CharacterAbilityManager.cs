using System.Collections.Generic;
using System.Linq;
using Abilities;

namespace Characters
{
    public class CharacterAbilityManager
    {
        public CharacterAbilityManager(Character character) => this.character = character;

        private Character character;
        
        public List<SlottedAbility> Abilities = new ();
        public void SortAbilities() => Abilities = Abilities.OrderBy(a => a.ability.ActionType).ThenBy(a => a.Priority).ToList();

        public List<SlottedAbility> GetOrderedAbilitiesByType(ActionTypes type_) => Abilities.Where(a => a.ability.ActionType == type_).OrderBy(a => a.Priority).ToList();
    }
}