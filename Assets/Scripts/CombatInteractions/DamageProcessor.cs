using Characters;
using Game;

namespace CombatInteractions
{
    public static class DamageProcessor
    {
        //Mitigate
        public static int MitigatePhysicalDamage(ICharacter user, ICharacter target, int rawPDamage) 
            => Formulas.GetMitigatedPhysicalDamage(rawPDamage, target.GetStat("pdef"), user.GetStat("atk"));
        
        public static int MitigateMagicalDamage(ICharacter user, ICharacter target, int rawMDamage)
            => Formulas.GetMitigatedMagicalDamage(rawMDamage, target.GetStat("mdef"), user.GetStat("matk"));

        //Raw
        public static void ApplyRawPhysicalDamage(ICharacter user, ICharacter target, int rawPDamage) 
            => ApplyPhysicalDamage(user, target, MitigatePhysicalDamage(user, target, rawPDamage));
        
        public static void ApplyRawMagicalDamage(ICharacter user, ICharacter target, int rawMDamage) 
            => ApplyMagicalDamage(user, target, MitigateMagicalDamage(user, target, rawMDamage));
        
        
        //Mitigated
        public static void ApplyPhysicalDamage(ICharacter user, ICharacter target, int pDamage)
        {
            int experience = Formulas.GetAttackExperience(user.GetRootCharacter(), target.GetRootCharacter(), pDamage);
            user.AddXP(experience);
            target.TakeDamage(pDamage);
        }
        
        public static void ApplyMagicalDamage(ICharacter user, ICharacter target, int mDamage)
        {
            int experience = Formulas.GetAttackExperience(user.GetRootCharacter(), target.GetRootCharacter(), mDamage);
            user.AddXP(experience);
            target.TakeDamage(mDamage);
        }
        
        //Other
        public static void ApplyHeal(ICharacter user, ICharacter target, int healAmount)
        {
            int experience = Formulas.GetHealExperience(user.GetRootCharacter(), target.GetRootCharacter(), healAmount);
            user.AddXP(experience);
            target.Heal(healAmount);
        }
    }
}