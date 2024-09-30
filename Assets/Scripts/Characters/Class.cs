using System.Collections.Generic;
using Combat;
using CoreLib.Complex_Types;
using Items;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Defines a class that a character can be. This includes the base stats, abilities, and equipment that the character can use.
    /// Later on, we'll expand onthe Advanced Class Features setting to enable split classing and associate individual levels with a specific class
    /// to allow either more flexibility: ie. Fighter 3, Mage 2, or the introduction of prestige classes: ie. Magician 5, Fire Mage 2
    ///
    /// Every entity in the game MUST have a class, including things like animals and monsters.
    ///
    /// At the moment, classes are static and you can't change a character's class. This may change in the future.
    /// </summary>
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
        [SerializeField]public MovementTypes MovementType; //Determines how the unit moves on the tactical map & affects combat bonuses. ie. Spears have a bonus against mounted units.
        [SerializeField]public List<Gender> AllowedGenders = new (); //Will probably be a question of art assets really.
        [SerializeField]public List<WeaponTypes> AllowedWeapons = new (); //Control itemization and progression
        
        [Space]
        [Header("Leveling")]
        [Space] [Header("Level 0 should be base stats for the class")]
        [SerializeField]public List<OnLevelOffsets> OnLevels = new ();
        
        
        [Header("Advanced Class Features")]
        public bool IsAdvancedClass => RequiredClassLevels.Count > 0;[Space]
        [SerializeField]public List<Pair<Class,int>> RequiredClassLevels = new ();
    }
}