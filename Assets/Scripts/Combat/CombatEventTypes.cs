using System.Collections.Generic;

namespace Combat
{
    public static class CombatEventTypes
    {

        //TODO move this into a scratch file until it's ready to be an enum
        public static List<string> types = new List<string>()
        {
            "OnSelfHitByEnemy",
            "OnSelfHitEnemy",

            "OnAllyHitByEnemy",
            "OnAllyHitEnemy",

            "OnSelfDeath",
            "OnAllyDeath",
            "OnEnemyDeath",

            "OnSelfHealed",
            "OnAllyHealed",
            "OnEnemyHealed",

            "OnSelfGuarded",
            "OnAllyGuarded",
            "OnEnemyGuarded",

            "OnSelfEvaded",
            "OnAllyEvaded",
            "OnEnemyEvaded",
            
            "OnCombatStart",
            "OnCombatEnd",
        };
    }
}