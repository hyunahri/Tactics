using System.Collections.Generic;
using Abilities;
using Characters;

namespace Items
{
    /// <summary>
    /// Single use item that applies some effect when used.
    /// Can only be used outside of combat.
    /// </summary>
    public class Consumable : Item
    {
        public Consumable(ConsumableTemplate template)
        {
            Name = template.Name;
            Description = template.Description;
            Icon = template.Icon;
            Size = template.Size;
            Value = template.Value;
            OnUseEffects = template.OnUseEffects;
        }
        
        
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