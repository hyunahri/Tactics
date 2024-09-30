using System.Collections.Generic;
using CoreLib.Complex_Types;
using Items;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Used to store the offsets that should be applied to a character when they level up.
    /// Each offset is associated with a particular Class. These are only setup in the editor and shouldn't be modified at runtime.
    /// 
    /// </summary>
    /// 
    [System.Serializable]
    public class OnLevelOffsets
    {
        [Header("Stats")]
        [SerializeField] public List<Pair<string, int>> StatIncreases = new List<Pair<string, int>>();
        
        [Header("Equipment")]
        [SerializeField] public List<EquipmentSlotTypes> EquipmentSlotUnlocks = new List<EquipmentSlotTypes>();
        
        [Header("Abilities")]
        [SerializeField] public List<string> AbilityUnlocks = new List<string>();
    }
}