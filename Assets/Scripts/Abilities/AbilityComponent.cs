using System.Collections.Generic;
using Characters;
using Combat;
using Game;

namespace Abilities
{
    public abstract class AbilityComponent
    {
        //Not sure if going to use these events yet, might just use tags instead
        public abstract List<string> PreEvents { get; set; }
        public abstract List<string> PostEvents { get; set; }

        public abstract bool TryToExecute(Battle combat, CharacterBattleAlias user, CharacterBattleAlias target);
    }
    
    public class PhysicalAttackComponent : AbilityComponent
    {
        public override List<string> PreEvents { get; set; } = new List<string>(){"combat_start"};
        public override List<string> PostEvents { get; set; } = new List<string>(){"combat_end"};
        
        public string DamageFormula = "max(0,[user.stats.atk] - [target.stats.def])";



        public override bool TryToExecute(Battle combat, CharacterBattleAlias user, CharacterBattleAlias target)
        {
            target.HP -= FormulaEvaluator.EvaluateExpression<int>(DamageFormula, user, target, new Dictionary<string, float>());
            return true;
        }
    }
    
    public class MagicAttackComponent : AbilityComponent
    {
        public string DamageFormula = "max(0,[user.stats.matk] - [target.stats.mdef])";

        public override List<string> PreEvents { get; set; }
        public override List<string> PostEvents { get; set; }

        public override bool TryToExecute(Battle combat, CharacterBattleAlias user, CharacterBattleAlias target)
        {
            target.HP -= FormulaEvaluator.EvaluateExpression<int>(DamageFormula, user, target, new Dictionary<string, float>());
            return true;
        }
    }
    
    public class HealAbilityComponent : AbilityComponent
    {
        public string HealFormula = "5 + [user.stats.matk]";

        public override List<string> PreEvents { get; set; }
        public override List<string> PostEvents { get; set; }

        public override bool TryToExecute(Battle combat, CharacterBattleAlias user, CharacterBattleAlias target)
        {
            target.HP += FormulaEvaluator.EvaluateExpression<int>(HealFormula, user, target, new Dictionary<string, float>());
            return true;
        }
    }
    
    
}