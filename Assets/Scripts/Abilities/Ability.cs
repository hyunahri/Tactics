using System.Collections.Generic;
using System.Linq;
using Characters;
using Combat;
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
        [SerializeField]public AbilityCoreTypes CoreType; //Whether this skill is active, passive, or a reaction
        [SerializeField]public List<AbilityTags> Tags; //Tags that can be used to filter abilities
        public bool HasTag(AbilityTags tag) => Tags.Contains(tag);

        [Space]
        [Header("Strategies")] //Processed in the order they are listed
        [SerializeField]public AbilityTargetingStrategy TargetingStrategy; //Quickly determines which characters are valid targets for an ability, result is unordered
        [SerializeField]public List<AbilityRequirementStrategy> FixedRequirements; //Must be satisfied for ability to be used on a target, defined by the ability
    }
    
    //<summary> Abilities must be slotted in order to be used by a character. Slotting allows a player to define additional requirements and prioritization strategies for when the ability will be used. </summary>
    public class SlottedAbility
    {
        [SerializeField]public Ability ability; //The ability that is slotted
        [SerializeField]public int Priority; //Determines the order in which abilities are checked for use by the character, lower is higher priority
        public bool IsPassive => ability.CoreType == AbilityCoreTypes.PASSIVE;
        
        [SerializeField]public List<AbilityRequirementStrategy> DefinedRequirements; //Must be satisfied for ability to be used on a target, defined by the player
        [SerializeField]public List<AbilityPrioritizationStrategy> PrioritizationStrategies; //Determines the order in which targets are selected
        
        //<summary> Determines if the ability can be used on any target, and which to prioritize </summary>
        public bool CanUse(BattleRound e, CharacterBattleAlias user, out CharacterBattleAlias? priorityTarget)
        {
            priorityTarget = null;
            if(user.IsKnockedOut || user.IsStunned || user.AP < ability.ActionPoints || user.PP < ability.ReactionPoints) 
                return false;
            if (ability.HasTag(AbilityTags.Magic) && user.IsSilenced)
                return false;

            // Get all valid targets
            var targets = ability.TargetingStrategy.GetValidTargets(e, user);
            if (targets.Count == 0) return false;

            // Cull targets that don't meet requirements
            var requirements = ability.FixedRequirements.Concat(DefinedRequirements);
            targets = targets.Where(t => requirements.All(req => req.Evaluate(e, user, t))).ToList();
            if (targets.Count == 0) return false;

            // Apply prioritization strategies
            IOrderedEnumerable<CharacterBattleAlias>? sortedTargets = null;
            for (int i = 0; i < PrioritizationStrategies.Count; i++)
            {
                var strategy = PrioritizationStrategies[i];
                var comparer = strategy.GetComparer(e, user);
                sortedTargets = i == 0 ? targets.OrderBy(t => t, comparer) : sortedTargets.ThenBy(t => t, comparer);
            }

            // If no prioritization strategies, apply a default ordering strategy, probably target the enemy directly in front and then move laterally later on
            sortedTargets ??= targets.OrderBy(t => 0);

            priorityTarget = sortedTargets.FirstOrDefault();
            return priorityTarget != null;
        }
    }
}