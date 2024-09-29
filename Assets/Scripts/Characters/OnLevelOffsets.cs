using System.Collections.Generic;
using CoreLib.Complex_Types;
using Items;
using UnityEngine;

namespace Characters
{
    [System.Serializable]
    public class OnLevelOffsets
    {
        public OnLevelOffsets()
        {
        }
        
        [Header("Stats")]
        [SerializeField] public List<Pair<string, int>> StatIncreases = new List<Pair<string, int>>();
        
        [Header("Equipment")]
        [SerializeField] public List<EquipmentSlotTypes> EquipmentSlotUnlocks = new List<EquipmentSlotTypes>();
        
        [Header("Abilities")]
        [SerializeField] public List<string> AbilityUnlocks = new List<string>();
    }
}