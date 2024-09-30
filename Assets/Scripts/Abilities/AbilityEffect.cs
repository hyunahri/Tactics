using System;
using System.Collections.Generic;
using Characters;
using Combat;
using CombatInteractions;
using Game;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class AbilityEffect
    {
        //Params
        //Description
        public virtual string GetDescription() => "Unimplemented";
        public virtual string GetDescriptionForUser(ICharacter user) => "Unimplemented";
        
        //Application
        public virtual void ApplyEffect(BattleRound e, ICharacter user, ICharacter target){}
    }
    
    [Serializable]
    public class PhysicalDamageEffect : AbilityEffect
    {
        public PhysicalDamageEffect(){}
        
        [Header("Params")]
        [Tooltip("Percentage of attack stat used in damage calculation. 1-100 as int")]
        [SerializeField][Range(0,300)] public int potency;

        //Description
        public override string GetDescription() => $"Deals {potency}% of user's attack as physical damage";
        public override string GetDescriptionForUser(ICharacter user) => $"Deals <b>{CalculateRawDamage(user)}</b> physical damage";

        //Application
        protected virtual int CalculateRawDamage(ICharacter user) => (int)Math.Ceiling(user.GetStat("atk") * (potency / 100f));
        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target) => DamageProcessor.ApplyRawPhysicalDamage(user, target, CalculateRawDamage(user));
    }
    
    [Serializable]
    public class MagicDamageEffect : AbilityEffect
    {
        //Params
        [Tooltip("Percentage of matk stat used in damage calculation. 1-100 as int")] 
        [SerializeField][Range(0,300)] public int potency;

        //Description
        public override string GetDescription() => $"Deals {potency}% of user's attack as magic damage";
        public override string GetDescriptionForUser(ICharacter user) => $"Deals <b>{CalculateRawDamage(user)}</b> magic damage";

        //Application
        protected virtual int CalculateRawDamage(ICharacter user) => (int)Math.Ceiling(user.GetStat("matk") * (potency / 100f));
        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target) => DamageProcessor.ApplyRawPhysicalDamage(user, target, CalculateRawDamage(user));
    }
    
    [Serializable]
    public class CustomDamageEffect : AbilityEffect
    {
        //Params
        public bool IsRawDamage; //If true, will pass through the normal raw damage applicator, otherwise applied directly.
        public bool IsPhysicalDamage; //If true, will use the physical damage applicator, otherwise the magic damage applicator.
        
        [Space]
        [SerializeField][TextArea(2,3)]public string DamageFormula; //ie "[user.stat.atk] * 2 + 5"
        
        //Description
        public override string GetDescription() => $"TODO";
        public override string GetDescriptionForUser(ICharacter user) => $"Deals <b>TODO</b> magic damage";

        //Application
        protected int CalculateDamage(ICharacter user, ICharacter target) => FormulaEvaluator.EvaluateExpression<int> (DamageFormula, user, target, new Dictionary<string, float>());

        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target)
        {
            int damage = CalculateDamage(user, target);
            if(IsRawDamage)
                if(IsPhysicalDamage)
                    DamageProcessor.ApplyRawPhysicalDamage(user, target, damage);
                else
                    DamageProcessor.ApplyRawMagicalDamage(user, target, damage);
            else
                if(IsPhysicalDamage)
                    DamageProcessor.ApplyPhysicalDamage(user, target, damage);
                else
                    DamageProcessor.ApplyRawMagicalDamage(user, target, damage);
        }
    }
    
    //heal effect
    [Serializable]
    public class MagicHealEffect : AbilityEffect
    {
        //Params
        [Tooltip("Percentage of matk stat used in damage calculation. 1-100 as int")] 
        [SerializeField][Range(0,300)] public int potency;

        //Description
        public override string GetDescription() => $"Heals {potency}% of user's magic attack";
        public override string GetDescriptionForUser(ICharacter user) => $"Heals <b>{CalculateRawHeal(user)}</b> health";

        //Application
        protected virtual int CalculateRawHeal(ICharacter user) => (int)Math.Ceiling(user.GetStat("matk") * (potency / 100f));
        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target) => DamageProcessor.ApplyHeal(user, target, CalculateRawHeal(user));
    }
    
    public class FlatHealEffect : AbilityEffect
    {
        //Params
        public int healAmount;

        //Description
        public override string GetDescription() => $"Heals {healAmount} health";
        public override string GetDescriptionForUser(ICharacter user) => $"Heals <b>{healAmount}</b> health";

        //Application
        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target) => DamageProcessor.ApplyHeal(user, target, healAmount);
    }
}