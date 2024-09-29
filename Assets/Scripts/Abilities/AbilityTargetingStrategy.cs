using System.Collections.Generic;
using System.Linq;
using Characters;
using Combat;

namespace Abilities
{
    //<summary> Determines which characters are valid targets for an ability, result is unordered </summary>
    public abstract class AbilityTargetingStrategy
    {
        public abstract string Name { get; }
        public abstract List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user);
    }

    /// <summary> Targets all characters in the enemy's unit </summary>
    public class EnemyTargeting : AbilityTargetingStrategy
    {
        public override string Name => "Enemy Targeting";

        public override List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user)
        {
            var targUnit = e.Battle.GetEnemyUnit(user);

            // Try to get alive targets from the front row
            var targets = targUnit.GetRow(0).Concat(targUnit.GetRow(1))
                .Where(c => !c.IsKnockedOut) // Filter out dead characters
                .Select(c => e.States[c]) // Convert to CharacterCombatState
                .ToList();
            return targets;
        }
    }
    
    //Enemy targeting same row
    public class SameRowEnemyTargeting : AbilityTargetingStrategy
    {
        public override string Name => "Same Row Enemy Targeting";

        public override List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user)
        {
            var targUnit = e.Battle.GetEnemyUnit(user);
            int row = targUnit.GetRow(user.GetRootCharacter());

            // Try to get alive targets from the front row
            var targets = targUnit.GetRow(row)
                .Where(c => !c.IsKnockedOut) // Filter out dead characters
                .Select(c => e.States[c]) // Convert to CharacterCombatState
                .ToList();
            return targets;
        }
    }
    
    //Enemy targeting different row
    public class DifferentRowEnemyTargeting : AbilityTargetingStrategy
    {
        public override string Name => "Different Row Enemy Targeting";

        public override List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user)
        {
            var targUnit = e.Battle.GetEnemyUnit(user);
            int row = targUnit.GetRow(user.GetRootCharacter());

            // Try to get alive targets from the front row
            var targets = targUnit.GetRow(1 - row)
                .Where(c => !c.IsKnockedOut) // Filter out dead characters
                .Select(c => e.States[c]) // Convert to CharacterCombatState
                .ToList();
            return targets;
        }
    }
    
    /// <summary>Must clear first row in order to target back row </summary>
    public class FrontalEnemyTargeting : AbilityTargetingStrategy
    {
        public override string Name => "Frontal Enemy Targeting";

        public override List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user)
        {
            var targUnit = e.Battle.GetEnemyUnit(user);

            // Try to get alive targets from the front row
            var targets = targUnit.GetRow(0)
                .Where(c => !c.IsKnockedOut) // Filter out dead characters
                .Select(c => e.States[c]) // Convert to CharacterCombatState
                .ToList();

            // If no alive targets in front row, try back row
            if (targets.Count == 0)
            {
                targets = targUnit.GetRow(1)
                    .Where(c => !c.IsKnockedOut)
                    .Select(c => e.States[c])
                    .ToList();
            }

            return targets;
        }
    }

    /// <summary>Must clear back row in order to target front row </summary>
    public class RearEnemyTargeting : AbilityTargetingStrategy
    {
        public override string Name => "Rear Enemy Targeting";

        public override List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user)
        {
            var targUnit = e.Battle.GetEnemyUnit(user);

            // Try to get alive targets from the front row
            var targets = targUnit.GetRow(1)
                .Where(c => !c.IsKnockedOut) // Filter out dead characters
                .Select(c => e.States[c]) // Convert to CharacterCombatState
                .ToList();

            // If no alive targets in front row, try back row
            if (targets.Count == 0)
            {
                targets = targUnit.GetRow(0)
                    .Where(c => !c.IsKnockedOut)
                    .Select(c => e.States[c])
                    .ToList();
            }

            return targets;
        }
    }
    
    public class FriendlyTargeting : AbilityTargetingStrategy
    {
        public override string Name => "Friendly Targeting";

        public override List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user)
        {
            var targUnit = e.Battle.GetOwnUnit(user);

            // Try to get alive targets from the front row
            var targets = targUnit.GetRow(0).Concat(targUnit.GetRow(1))
                .Where(c => !c.IsKnockedOut) // Filter out dead characters
                .Select(c => e.States[c]) // Convert to CharacterCombatState
                .ToList();
            return targets;
        }
    }
    
    //friendly targeting same row
    public class SameRowFriendlyTargeting : AbilityTargetingStrategy
    {
        public override string Name => "Same Row Friendly Targeting";

        public override List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user)
        {
            var targUnit = e.Battle.GetOwnUnit(user);
            int row = targUnit.GetRow(user.GetRootCharacter());

            // Try to get alive targets from the front row
            var targets = targUnit.GetRow(row)
                .Where(c => !c.IsKnockedOut) // Filter out dead characters
                .Select(c => e.States[c]) // Convert to CharacterCombatState
                .ToList();
            return targets;
        }
    }
    
    //friendly targeting different row
    public class DifferentRowFriendlyTargeting : AbilityTargetingStrategy
    {
        public override string Name => "Different Row Friendly Targeting";

        public override List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user)
        {
            var targUnit = e.Battle.GetOwnUnit(user);
            int row = targUnit.GetRow(user.GetRootCharacter());

            // Try to get alive targets from the front row
            var targets = targUnit.GetRow(1 - row)
                .Where(c => !c.IsKnockedOut) // Filter out dead characters
                .Select(c => e.States[c]) // Convert to CharacterCombatState
                .ToList();
            return targets;
        }
    }
    
    public class SelfTargeting : AbilityTargetingStrategy
    {
        public override string Name => "Self Targeting";

        public override List<CharacterBattleAlias> GetValidTargets(BattleRound e, CharacterBattleAlias user) => new() { user };
    }
}