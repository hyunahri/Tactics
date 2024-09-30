using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Consumable")]
    public class ConsumableTemplate : ScriptableObject
    {
        [SerializeField]public string Name;
        [SerializeField]public string Description;
        
         [SerializeField]public Sprite Icon;
        
        [Header("Values")]
        [SerializeField]public int Size = 1;
        [SerializeField]public int Value = 100;
        
        [Header("Effects")]
        [SerializeReference]public List<AbilityEffect> OnUseEffects = new List<AbilityEffect>();
    }
}