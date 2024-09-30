using System.Collections.Generic;
using Abilities;
using Characters;

namespace Items
{
    /// <summary>
    /// Single use item that applies some effect when used.
    /// Can only be used outside of combat.
    /// </summary>
    public class Consumable
    {
        public List<AbilityEffect> OnUseEffects = new List<AbilityEffect>();

        public void Use(Character character)
        {
            foreach (var effect in OnUseEffects)
            {
                effect.ApplyEffect(null, character, character);
            }
        }
    }
}