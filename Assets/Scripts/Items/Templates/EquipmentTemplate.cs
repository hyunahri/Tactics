using System.Collections.Generic;
using Abilities;
using CoreLib.Complex_Types;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Equipment")]
    public class EquipmentTemplate : ScriptableObject
    {
        [Header("Identity")] 
        [SerializeField]public string Name;
        [SerializeField]public string Description;
        [SerializeField]public Sprite Icon;
        
        [Header("Item")]
        [SerializeField]public int Value;
        [SerializeField]public int Weight;
        
        [Space]
        [Header("Slot")]
        [SerializeField]public EquipmentSlotTypes Slot;

        [Space] 
        [Header("Durability")] 
        [SerializeField]public bool UseDurability = false;
        [SerializeField]public bool CanBeRepaired = false;
        [SerializeField][Range(0,1f)] public float ChanceToDegradeOnUse = 0f;
        [SerializeField]public int MaxDurability = 1;
        
        [Space]
        [Header("Features")] 
        [SerializeField]public List<Ability> Abilities = new List<Ability>();
        [SerializeField]public List<Pair<string,int>> Bonuses = new List<Pair<string, int>>();
    }
}