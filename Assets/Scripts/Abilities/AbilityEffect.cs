using System;
using System.Collections.Generic;
using Characters;
using Combat;
using CombatInteractions;
using Game;
using UnityEngine;

namespace Abilities
{
    [System.Serializable]
    public class AbilityEffect
    {
        //Params
        //Description
        public virtual string GetDescription() => "Unimplemented";
        public virtual string GetDescriptionForUser(ICharacter user) => "Unimplemented";
        
        //Application
        public virtual void ApplyEffect(BattleRound e, ICharacter user, ICharacter target){}
    }
    
    [System.Serializable]
    public class PhysicalDamageEffect : AbilityEffect
    {
        public PhysicalDamageEffect(){}
        
        [Header("Params")]
        [Tooltip("Percentage of attack stat used in damage calculation. 1-100 as int")]
        [SerializeField][Range(0,100)] public int potency;

        //Description
        public override string GetDescription() => $"Deals {potency}% of user's attack as physical damage";
        public override string GetDescriptionForUser(ICharacter user) => $"Deals <b>{CalculateRawDamage(user)}</b> physical damage";

        //Application
        protected virtual int CalculateRawDamage(ICharacter user) => (int)Math.Ceiling(user.GetStat("atk") * (potency / 100f));
        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target) => DamageProcessor.ApplyRawPhysicalDamage(user, target, CalculateRawDamage(user));
    }
    
    [System.Serializable]
    public class MagicDamageEffect : AbilityEffect
    {
        //Params
        [Tooltip("Percentage of matk stat used in damage calculation. 1-100 as int")] 
        [SerializeField][Range(0,100)] public int potency;

        //Description
        public override string GetDescription() => $"Deals {potency}% of user's attack as magic damage";
        public override string GetDescriptionForUser(ICharacter user) => $"Deals <b>{CalculateRawDamage(user)}</b> magic damage";

        //Application
        protected virtual int CalculateRawDamage(ICharacter user) => (int)Math.Ceiling(user.GetStat("matk") * (potency / 100f));
        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target) => DamageProcessor.ApplyRawPhysicalDamage(user, target, CalculateRawDamage(user));
    }
    
    [System.Serializable]
    public class CustomDamageEffect : AbilityEffect
    {
        //Params
        public bool IsRawDamage; //If true, will pass through the normal raw damage applicator, otherwise applied directly.
        public bool IsPhysicalDamage; //If true, will use the physical damage applicator, otherwise the magic damage applicator.
        [Space]
        [SerializeField][TextArea(2,3)]public string DamageFormula;
        
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
}