using System.Collections.Generic;
using Combat;
using CoreLib.Complex_Types;
using Items;
using UnityEngine;

namespace Characters
{
    //TODO add cleanup method to purge all duplicates
    [CreateAssetMenu(fileName = "Class Template", menuName = "Characters/Class")]
    public class Class : ScriptableObject
    {
        public string Name;
        public string Description;
        public bool IsSentient;
        
        [Space]
        public string Key;
        public Sprite Icon;

        [Space]
        [Header("Rules")]
        [SerializeField]public MovementTypes MovementType;
        [SerializeField]public List<Gender> AllowedGenders = new ();
        [SerializeField]public List<WeaponTypes> AllowedWeapons = new ();
        
        [Space]
        [Header("Leveling")]
        [Space] [Header("Level 0 should be base stats for the class")]
        [SerializeField]public List<OnLevelOffsets> OnLevels = new ();
        
        
        [Header("Advanced Class Features")]
        public bool IsAdvancedClass => RequiredClassLevels.Count > 0;[Space]
        [SerializeField]public List<Pair<Class,int>> RequiredClassLevels = new ();
    }
}