using System;
using System.Collections.Generic;
using CoreLib.Complex_Types;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities
{
    //<summary> Data storage object defining an ability, immutable at runtime.</summary>
    [CreateAssetMenu(menuName = "Abilities/Ability")]
    public class Ability : ScriptableObject
    {
        [Header("Identity")]
        public string Name;
        public string Description; 
        
        [Space]
        [Header("Costs")]
        [SerializeField]public int ActionPoints;
        [SerializeField]public int ReactionPoints;
        
        
        [Space]
        [Header("Classification")]
        [SerializeField]public ActionTypes ActionType; //The type of action this ability is, used for determining if it can be used in a given context
        [SerializeField]public List<AbilityTags> Tags; //Tags that can be used to filter abilities
        public bool HasTag(AbilityTags tag) => Tags.Contains(tag);

        [Space]
        [Header("Collections - Be Careful")]
        [Space]
        [Space]
        [Header("Strategies")] //Processed in the order they are listed
        [Tooltip("Quickly determines which characters are valid targets for an ability, result is unordered")]
        [SerializeReference]public List<AbilityTargetingStrategy> TargetingStrategies = new List<AbilityTargetingStrategy>(); //TODO for now only first strategy is actually used.
        
        [Tooltip("Must be satisfied for ability to be used on a target, defined by the ability")]
        [Space]
        [Header("Requirements")]
        [SerializeReference]public List<AbilityRequirementStrategy> FixedRequirements = new List<AbilityRequirementStrategy>(); //
        
        [Tooltip("Effects that are applied to targets when the ability is used")]
        [Space]
        [Header("Effects")]
        [SerializeReference]public List<AbilityEffect> Effects = new List<AbilityEffect>(); 
    }
}