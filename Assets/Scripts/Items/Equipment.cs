using System;
using Abilities;
using Characters;
using CoreLib.Complex_Types;
using UnityEngine;

namespace Items
{
    public class Equipment : Item, IAbilitySource
    {
        public Equipment(EquipmentTemplate data)
        {
            Data = data;
            Name = data.Name;
            Description = data.Description;
            CurrentDurability = data.MaxDurability;
            Bonuses = new DefaultDict<string, int>(() => 0, StringComparer.OrdinalIgnoreCase);
            foreach (var bonus in data.Bonuses)
            {
                Bonuses[bonus.Item1] = bonus.Item2;
            }
        }

        //Instance
        public readonly EquipmentTemplate Data;
        public Character? EquippedCharacter;
        public bool IsEquipped => EquippedCharacter != null;
        
        //
        public string GetName() => Name;
        public override bool IsStackable => true;

        //Bonuses
        public DefaultDict<string, int> Bonuses = new DefaultDict<string, int>(() => 0, StringComparer.OrdinalIgnoreCase); //Defines bonuses to a characters stats while the item is equipped
        public int GetDurabilityScaledBonus(string stat) =>  Data.UseDurability ? Mathf.FloorToInt(Bonuses[stat] * DurabilityPercentage) : Bonuses[stat]; //Returns the bonus scaled by durability
        public int GetUnscaledBonus(string stat) => Bonuses[stat]; //Returns the bonus without scaling by durability
         
        //Durability
        public int CurrentDurability;
        public void Repair() => CurrentDurability = Data.MaxDurability;
        public float DurabilityPercentage => (float)CurrentDurability / Data.MaxDurability;

        //Abilities
        public bool EligibleForAbility(Ability ability, ICharacter character) => EquippedCharacter != null && EquippedCharacter == character;
    }
}
