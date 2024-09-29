using System;
using Characters;

namespace Game
{
    public static class Formulas
    {
        public static int ExperienceToNextLevel(int nextLevel) => (int) ((nextLevel * (nextLevel * 0.4f) + 5) * 90);
        public static int ExperiencePerKill(int levelOfAttacker, int levelOfDefender) => 
            (int) (levelOfDefender * 100 * Math.Max(0.1f, 1 + (levelOfAttacker - levelOfDefender) * 0.05f));

        public static int GetAttackExperience(Character attacker, Character defender, int damageDone) //Should also work for heals
        {
            int levelDifference = defender.Level - attacker.Level;
            int experience = ExperiencePerKill(attacker.Level, defender.Level);
            return (int)(experience * (float)((float)damageDone / defender.MaxHP));
        }
    }
}