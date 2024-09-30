using System.Collections.Generic;
using Abilities;
using Characters;

namespace Items
{
    public class Consumable
    {
        public List<AbilityEffect> OnUseEffects = new List<AbilityEffect>();

        public void Use(Character character)
        {
            
        }
    }
}