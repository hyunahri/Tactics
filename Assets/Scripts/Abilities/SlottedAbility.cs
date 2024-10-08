﻿using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Combat;
using UnityEngine;

namespace Abilities
{
    ///<summary>
    /// Abilities must be slotted in order to be used by a character. Slotting allows a player to define additional requirements and prioritization strategies for when the ability will be used. </summary>
    public class SlottedAbility
    {
        public OwnedAbility Owned;
        public Ability ability => Owned.Ability; //The ability that is slotted
        [SerializeField] public int Priority; //Determines the order in which abilities are checked for use by the character, lower is higher priority
        public bool IsPassive => ability.ActionType == ActionTypes.Passive; //Passive abilities are automatically slotted so they can modify the character's stats, but don't need to be checked for use

        [SerializeField] public List<AbilityRequirementStrategy> DefinedRequirements; //Must be satisfied for ability to be used on a target, defined by the player
        [SerializeField] public List<AbilityPrioritizationStrategy> PrioritizationStrategies; //Determines the order in which targets are selected


        //todo All the below goes to a new class

        public bool CanUse(BattleRound r, CharacterBattleAlias user, out CharacterBattleAlias? priorityTarget)
        {
            priorityTarget = null;

            if(IsPassive) return false; //Passive abilities are automatic

            //Check hard requirements
            if (user.IsKnockedOut || user.IsStunned || user.AP < ability.ActionPoints || user.PP < ability.PassivePoints)
                return false;
            if (ability.HasTag(AbilityTags.Magic) && user.IsSilenced)
                return false;

            // Get all valid targets
            var validAliases = ability.TargetingStrategies.First().GetValidTargets(r, user); //TODO use multiple strategies
            if (validAliases.Count == 0) return false;

            // Cull targets that don't meet requirements
            var requirements = ability.FixedRequirements.Concat(DefinedRequirements);
            validAliases = validAliases.Where(t => requirements.All(req => req.Evaluate(r, user, t))).ToList();

            if (validAliases.Count == 0) return false;

            // Apply prioritization strategies
            IOrderedEnumerable<CharacterBattleAlias>? sortedTargets = null;
            for (int i = 0; i < PrioritizationStrategies.Count; i++)
            {
                var strategy = PrioritizationStrategies[i];
                var comparer = strategy.GetComparer(r, user);
                sortedTargets = i == 0 ? validAliases.OrderBy(t => t, comparer) : sortedTargets.ThenBy(t => t, comparer);
            }

            // If no prioritization strategies, apply a default ordering strategy, probably target the enemy directly in front and then move laterally later on
            sortedTargets ??= sortedTargets.OrderBy(t => t.GetRootCharacter().Name);

            return ability.ActionType switch
            {
                ActionTypes.Preparation => CanUsePreparation(r, sortedTargets, user, out priorityTarget),
                ActionTypes.Primary => CanUsePrimary(r, sortedTargets, user, out priorityTarget),
                ActionTypes.Reaction => CanUseReaction(r, sortedTargets, user, out priorityTarget),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        //<summary> Determines if the ability can be used as a preparation, and which target to prioritize </summary>
        private bool CanUsePreparation(BattleRound e, IOrderedEnumerable<CharacterBattleAlias> set, CharacterBattleAlias user, out CharacterBattleAlias? priorityTarget)
        {
            priorityTarget = null;

            // Get the primary action that triggered the reaction
            var primaryAction = e.RootAction;

            //Can't react to self
            if (primaryAction.user.GetRootCharacter().Unit == user.GetRootCharacter().Unit) return false;

            //Exit early if the ability cannot affect either the primary action's user or the target
            if (set.Contains(primaryAction.user) == false && set.Contains(primaryAction.GetTarget()) == false)
                return false;

            return priorityTarget != null;
        }

        //<summary> Determines if the ability can be used on any target, and which to prioritize </summary>
        private bool CanUsePrimary(BattleRound e, IOrderedEnumerable<CharacterBattleAlias> set, CharacterBattleAlias user, out CharacterBattleAlias? priorityTarget)
        {
            priorityTarget = set.FirstOrDefault();
            return priorityTarget != null;
        }

        //

        //<summary> Determines if the ability can be used as a reaction, and which target to prioritize </summary>
        private bool CanUseReaction(BattleRound e, IOrderedEnumerable<CharacterBattleAlias> set, CharacterBattleAlias user, out CharacterBattleAlias? priorityTarget)
        {
            priorityTarget = null;

            // Get the primary action that triggered the reaction
            var primaryAction = e.RootAction;

            //Can't react to self
            if (primaryAction.user.GetRootCharacter().Unit == user.GetRootCharacter().Unit) return false;

            // Exit early if the ability cannot affect either the primary action's user or the target
            if (!set.Any(alias => alias.GetRootCharacter() == primaryAction.user.GetRootCharacter() ||
                                  alias.GetRootCharacter() == primaryAction.GetTarget().GetRootCharacter()))
                return false;


            //todo check for tags and specifics about the ability and primary action
            return priorityTarget != null;
        }
    }
}