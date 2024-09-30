using System;
using Characters;

namespace Game
{
    /// <summary>
    /// Stick formulas here so they can be consistent across the game.
    /// </summary>
    public static class Formulas
    {
        public static int ExperienceToNextLevel(int nextLevel) => (int) ((nextLevel * (nextLevel * 0.4f) + 5) * 90);
        public static int ExperiencePerKill(int levelOfAttacker, int levelOfDefender) 
            => (int) (levelOfDefender * 100 * Math.Max(0.1f, 1 + (levelOfAttacker - levelOfDefender) * 0.05f));

        public static int GetAttackExperience(Character attacker, Character defender, int damageDone) //Should also work for heals
        {
            int experience = ExperiencePerKill(attacker.Level, defender.Level);
            return (int)(experience * ((float)damageDone / defender.MaxHP));
        }
        
        public static int GetHealExperience(Character healer, Character target, int healAmount)
        {
            //Don't give experience for overhealing
            int maxHeal = target.MaxHP - target.CurrentHP;
            int compensatedHealAmount = Math.Min(healAmount, maxHeal); 
            return (int)(Globals.XPModifier_Healing * GetAttackExperience(healer, target, compensatedHealAmount));
        }

        public static int GetMitigatedPhysicalDamage(int rawDamage, int pDef, int attackPotencyAsInt) => (int)Math.Ceiling(rawDamage - pDef * (attackPotencyAsInt / 100f));
        public static int GetMitigatedMagicalDamage(int rawDamage, int pDef, int attackPotencyAsInt) => (int)Math.Ceiling(rawDamage - pDef * (attackPotencyAsInt / 100f));

    }
}