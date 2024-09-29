using System;
using CoreLib.Complex_Types;

namespace Characters
{
    public class CharacterStatsManager
    {
        public CharacterStatsManager(Character character)
        {
            this.character = character;
        }

        private Character character;
        
        public DefaultDict<string, int> ClassStats = new DefaultDict<string, int>(() => 0, StringComparer.OrdinalIgnoreCase); //Stats from class
        public DefaultDict<string, int> PermanentOffsets = new DefaultDict<string, int>(() => 0, StringComparer.OrdinalIgnoreCase); //Permanent offsets from special events and consumables
        public DefaultDict<string, int> EquipmentOffsets = new DefaultDict<string, int>(() => 0, StringComparer.OrdinalIgnoreCase); //Offsets from equipped equipment
        public DefaultDict<string, int> StatusOffsets = new DefaultDict<string, int>(() => 0); //Offsets from status effects
        public int GetStat(string stat) => ClassStats[stat] + EquipmentOffsets[stat] + PermanentOffsets[stat] + StatusOffsets[stat];

        public void RebuildClassStats()
        {
            //Rebuild stats after changing class
            ClassStats.Clear();
            for (int i = 0; i < character.Level; i++)
            {
                foreach (var onLevel in character.Class.OnLevels)
                {
                    if (onLevel is null)
                        continue;
                    ApplyOnLevelOffsets(onLevel);
                }
            }
        }
        
        public void ApplyOnLevelOffsets(OnLevelOffsets onLevelOffsets)
        {
            foreach (var pair in onLevelOffsets.StatIncreases)
                ClassStats[pair.Item1] += pair.Item2;
            character.OnChanged.Invoke();
        }

        public void RebuildEquipmentOffsets()
        {  //Rebuild offsets after changing equipment
            EquipmentOffsets.Clear();
            foreach (var slot in character.EquipmentManager.GetSlots())
            {
                if (slot.Equipment == null) continue;
                foreach (var pair in slot.Equipment.Bonuses)
                    EquipmentOffsets[pair.Key] += pair.Value;
            }
            character.OnChanged.Invoke();
        }
        
        
    }
}