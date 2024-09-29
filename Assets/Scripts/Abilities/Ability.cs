using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    //<summary> Data storage object defining an ability, immutable at runtime.</summary>
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
        [Header("Strategies")] //Processed in the order they are listed
        [SerializeField]public AbilityTargetingStrategy TargetingStrategy; //Quickly determines which characters are valid targets for an ability, result is unordered
        [SerializeField]public List<AbilityRequirementStrategy> FixedRequirements; //Must be satisfied for ability to be used on a target, defined by the ability
    }
    
    //<summary> Abilities must be slotted in order to be used by a character. Slotting allows a player to define additional requirements and prioritization strategies for when the ability will be used. </summary>
}