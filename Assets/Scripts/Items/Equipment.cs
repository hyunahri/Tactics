using System;
using Characters;
using CoreLib.Complex_Types;
using UnityEngine;

namespace Items
{
    public class Equipment
    {
        public EquipmentSlotTypes Slot;
        public Character? EquippedCharacter;
        public bool IsEquipped => EquippedCharacter != null;

        public DefaultDict<string, int> Bonuses = new DefaultDict<string, int>(() => 0, StringComparer.OrdinalIgnoreCase);
        public int GetBonus(string stat) =>  UseDurability ? Mathf.FloorToInt(Bonuses[stat] * DurabilityPercentage) : Bonuses[stat];
        
        //Durability
        public bool UseDurability => MaxDurability > 0;
        public int CurrentDurability;
        public int MaxDurability;

        
        public bool CanBeRepaired = true;
        public void Repair() => CurrentDurability = MaxDurability;
        
        public float DurabilityPercentage => (float)CurrentDurability / MaxDurability;
    }
}