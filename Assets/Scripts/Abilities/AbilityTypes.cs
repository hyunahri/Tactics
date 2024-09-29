namespace Abilities
{
    public enum AbilityTypes
    {
        Attack,
        Buff,
        Debuff,
        Heal,
        Standby
    }

    public enum Scope
    {
        SELF,
        ALLY,
        ENEMY,
        ANY
    }

    public enum ActionTypes
    {
        Preparation,
        Primary,
        Reaction,
        Passive
    }
    
    public enum AbilityTags
    {
        BlockPreActions,
        BlockPostActions,
        Offensive,
        Defensive,
        Physical,
        Magic,
        Healing,
    }
} 