using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/EquipmentTemplate")]
    public class EquipmentTemplate : ScriptableObject
    {
        [Header("Identity")] 
        [SerializeField]public string Name;
        [SerializeField]public string Description;
        
        [Space]
        [Header("Slot")]
        [SerializeField]public EquipmentSlotTypes Slot;

        [Space] 
        [Header("Durability")] 
        [SerializeField]public bool UseDurability = false;
        [SerializeField]public bool CanBeRepaired = false;
        [SerializeField][Range(0,1f)] public float ChanceToDegradeOnUse = 0f;
        [SerializeField]public int MaxDurability;
        
        [Space]
        [Header("Features")] 
        [SerializeField]public List<Ability> Abilities = new List<Ability>();
        [SerializeField]public List<KeyValuePair<string,int>> Bonuses = new List<KeyValuePair<string, int>>();
    }
}